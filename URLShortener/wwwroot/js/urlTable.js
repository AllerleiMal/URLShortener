let pageSize = 10;
let pageNumber = 1;
let lastPage;

async function getPagedData() {
    return $.ajax({
        type:"GET",
        url: window.location.origin + `/url/getPage?pageSize=${pageSize}&pageNumber=${pageNumber}`,
        dataType: "json"
    });
}

function addUrlMappingInTable(table, mapping){
    let row = table.insertRow();
    let shortUrlCell = row.insertCell()
    let longUrlCell = row.insertCell();
    let clickCounterCell = row.insertCell();
    
    shortUrlCell.innerHTML = `<a class="short-url" href="${window.location.origin}/${mapping.ShortUrlCode}">${window.location.origin}/${mapping.ShortUrlCode}</a>`;
    longUrlCell.innerHTML = `<a class="long-url" href="${mapping.LongUrl}">${mapping.LongUrl}</a>`;
    clickCounterCell.innerHTML = mapping.ClickCounter;
}
async function refreshTable(){
    let response;
    // let promise = getPagedData(pageNumber, pageSize);
    await getPagedData(pageNumber, pageSize).then(data => {
        response = JSON.parse(data);
    });
    
    let table = document.getElementById('url-table').getElementsByTagName('tbody')[0];
    table.innerHTML = "";

    console.log(response)
    
    response.Data.forEach(mapping => {
        addUrlMappingInTable(table, mapping);
    })
}

async function main(){
    await refreshTable()
}

main()