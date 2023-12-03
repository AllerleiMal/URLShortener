using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using URLShortener.Mapping;
using URLShortener.Repositories;
using URLShortener.Services;
using ISession = NHibernate.ISession;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

var sessionFactory = Fluently.Configure()
    .Database(MySQLConfiguration.Standard.ConnectionString(builder.Configuration.GetConnectionString("DefaultConnection")))
    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UrlMappingMap>())
    .ExposeConfiguration(configuration =>
    {
        var update = new SchemaUpdate(configuration);
        update.Execute(false, true);
    })
    .BuildSessionFactory();

builder.Services.AddSingleton(sessionFactory);
builder.Services.AddScoped<ISession, ISession>(_ => sessionFactory.OpenSession());

builder.Services.AddTransient<IUrlShortener, UrlShortener>();
builder.Services.AddScoped<IUrlMappingRepository, UrlMappingRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
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