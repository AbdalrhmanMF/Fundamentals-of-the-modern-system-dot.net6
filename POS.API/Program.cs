using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using MVCCore;
using POS.BL.UOW;
using POS.DAL.Database;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//string connString = builder.Configuration.GetConnectionString("POSConnection");

builder.Services.AddDbContext<POSContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("POSConnection"),
        x => x.MigrationsAssembly(typeof(POSContext).Assembly.FullName)));



builder.Services.AddLocalization();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
//builder.Services.AddTransient(typeof(IBaseRepository<>),typeof(BaseRepository<>));


builder.Services.AddMvc()
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(JsonStringLocalizerFactory));
    });


builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultuers = new[]
    {
        new CultureInfo("ar-EG"),
        new CultureInfo("en-US"),
        new CultureInfo("de-DE"),
    };

    options.DefaultRequestCulture = new RequestCulture(culture: supportedCultuers[0]);
    options.SupportedCultures = supportedCultuers;
});

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var supportedCultures = new[] { "ar-EG", "en-US", "de-DE" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);


app.UseAuthorization();

app.MapControllers();

app.Run();
