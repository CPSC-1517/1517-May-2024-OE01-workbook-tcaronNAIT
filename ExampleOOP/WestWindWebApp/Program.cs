using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using WestWindLibrary;
using WestWindWebApp.Components;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("WWDB");

//Add the Services and provide the options for our DBConnection
builder.Services.WestWindExtensionServices(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
