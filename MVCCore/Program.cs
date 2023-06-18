using POS.BL.UOW;
using POS.DAL.Database;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using MVCCore;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using MVCCore.Middleware;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//string connString = builder.Configuration.GetConnectionString("POSConnection");

builder.Services.AddDbContext<POSContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("POSConnection"),
        x => x.MigrationsAssembly(typeof(POSContext).Assembly.FullName)));


builder.Services.AddLocalization();
builder.Services.AddDistributedMemoryCache(); 
builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();

builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(op =>
    {
        op.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(JsonStringLocalizerFactory));
    });

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultuers = new[]
    {
        new CultureInfo("ar-EG"),
        new CultureInfo("en-US"),
        new CultureInfo("de-DE"),
        //new CultureInfo("fr"),
        //new CultureInfo("es"),
    };
    //options.DefaultRequestCulture = new RequestCulture(culture: supportedCultuers[0], uiCulture: supportedCultuers[0]);
    options.SupportedCultures = supportedCultuers;
    options.SupportedUICultures = supportedCultuers;
});

//builder.Services.AddTransient(typeof(IBaseRepository<>),typeof(BaseRepository<>));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

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

    var supportedCultuers = new[] { "ar-EG", "en-US", "de-DE", };
var LoclizationOptions = new RequestLocalizationOptions()
    //.SetDefaultCulture(supportedCultuers[0])
    .AddSupportedCultures(supportedCultuers)
    .AddSupportedUICultures(supportedCultuers);

app.UseRequestLocalization(LoclizationOptions);

 
app.UseAuthorization();



app.UseRequestCulture();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
