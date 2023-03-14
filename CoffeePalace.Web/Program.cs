using CoffeePalace.Data;
using CoffeePalace.Models;
using CoffeePalace.Services;
using CoffeePalace.Web;
using CoffeePalace.Web.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureData(builder.Configuration);
builder.Services.ConfigureModels(builder.Configuration);
builder.Services.ConfigureServices(builder.Configuration);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

using var scope = app.Services.CreateScope();

var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
if (dbContext is null) throw new Exception("During startup dbContext was null");

var seeder = new Seeder(dbContext);

seeder.Seed();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();