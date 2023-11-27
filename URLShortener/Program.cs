using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using URLShortener.Mapping;
using ISession = NHibernate.ISession;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var sessionFactory = Fluently.Configure()
    .Database(MySQLConfiguration.Standard.ConnectionString(builder.Configuration.GetConnectionString("DefaultConnection")))
    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UrlMappingMap>())
    .ExposeConfiguration(configuration =>
    {
        var update = new SchemaUpdate(configuration);
        update.Execute(false, true);
    })
    .BuildSessionFactory();

builder.Services.AddSingleton<ISessionFactory>(sessionFactory);
builder.Services.AddScoped<ISession, ISession>(_ => sessionFactory.OpenSession());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Url}/{action=Index}/{id?}");

app.Run();