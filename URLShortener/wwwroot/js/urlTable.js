let pageSize = 10;
let pageNumber = 1;
let lastPage;
let firstPagePicker = document.getElementById("first-page-picker");
let previousPagePicker = document.getElementById("previous-page-picker");
let nextPagePicker = document.getElementById("next-page-picker");
let lastPagePicker = document.getElementById("last-page-picker");
let currentPageNumberSpan = document.getElementById("current-page-number");

async function getPagedData() {
    return $.ajax({
        type: "GET",
        url: window.location.origin + `/url/getPage?pageSize=${pageSize}&pageNumber=${pageNumber}`,
        dataType: "json"
    });
}

async function deleteMapping(id) {
    return $.ajax({
        type: "GET",
        url: window.location.origin + `/url/delete?id=${id}`
    });
}

async function getNextPage() {
    pageNumber++;
    if (pageNumber >= lastPage) {
        await getLastPage();
        return;
    }

    await refreshTable();
}

async function getPreviousPage() {
    pageNumber--;
    if (pageNumber <= 1) {
        await getFirstPage();
        return
    }

    await refreshTable();
}

async function getFirstPage() {
    pageNumber = 1;

    await refreshTable();
}

async function getLastPage() {
    pageNumber = lastPage;

    await refreshTable();
}

function setNextAndLastPagePickersDisplayed(isDisplayed) {
    nextPagePicker.hidden = !isDisplayed;
    lastPagePicker.hidden = !isDisplayed;
}

function setPreviousAndFirstPagePickersDisplayed(isDisplayed) {
    previousPagePicker.hidden = !isDisplayed;
    firstPagePicker.hidden = !isDisplayed;
}

function addUrlMappingInTable(table, mapping) {
    let row = table.insertRow();
    let shortUrlCell = row.insertCell()
    let longUrlCell = row.insertCell();
    let clickCounterCell = row.insertCell();
    let creationDateCell = row.insertCell();
    let deleteBtnCell = row.insertCell();


    shortUrlCell.innerHTML = `<a class="short-url" href="${window.location.origin}/${mapping.ShortUrlCode}">${window.location.origin}/${mapping.ShortUrlCode}</a>`;
    longUrlCell.innerHTML = `<a class="long-url" href="${mapping.LongUrl}">${mapping.LongUrl}</a>`;
    clickCounterCell.innerHTML = mapping.ClickCounter;
    creationDateCell.innerHTML = mapping.CreationDate.replace("T", " ");
    let deleteBtn = document.createElement('a');
    deleteBtn.textContent = 'Delete';
    deleteBtn.className = 'delete-mapping-btn';
    deleteBtn.addEventListener('click', async function (e) {
        await deleteMapping(mapping.Id);
        await refreshTable();
    });
    deleteBtnCell.appendChild(deleteBtn);
}

async function refreshTable() {
    let response;
    await getPagedData(pageNumber, pageSize).then(data => {
        response = JSON.parse(data);
    });
    document.getElementById('current-records-info').textContent = response.TotalRecords > 0 ? `Records ${(pageNumber - 1) * pageSize + 1} 
        - ${Math.min(pageNumber * pageSize, response.TotalRecords)} of ${response.TotalRecords}` : 'No records provided';
    
    lastPage = Math.ceil(response.TotalRecords / pageSize);
    
    currentPageNumberSpan.textContent = pageNumber;
    let hasNextPage = response.Data.length === pageSize && pageSize * pageNumber < response.TotalRecords;
    setNextAndLastPagePickersDisplayed(hasNextPage);
    let hasPreviousPage = pageNumber > 1
    setPreviousAndFirstPagePickersDisplayed(hasPreviousPage);

    let table = document.getElementById('url-table').getElementsByTagName('tbody')[0];
    table.innerHTML = "";

    response.Data.forEach(mapping => {
        addUrlMappingInTable(table, mapping);
    })
}

async function main() {
    await refreshTable()
    firstPagePicker.addEventListener('click', getFirstPage);
    previousPagePicker.addEventListener('click', getPreviousPage);
    nextPagePicker.addEventListener('click', getNextPage);
    lastPagePicker.addEventListener('click', getLastPage);
    let pageSizePickers = document.getElementsByClassName('page-sizes');

    for (let i = 0; i < pageSizePickers.length; ++i) {
        pageSizePickers[i].addEventListener('click', function (e) {
            pageSize = parseInt(e.target.textContent);
            pageNumber = 1;
            refreshTable();
        });
    }
}

main()