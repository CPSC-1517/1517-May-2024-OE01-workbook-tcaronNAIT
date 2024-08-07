using ExampleWestWind.Components;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using MudBlazor.Services;
using WestWindDB;
using WestWindDB.BLL;
using WestWindDB.DAL;

var builder = WebApplication.CreateBuilder(args);

//var connectionString = builder.Configuration.GetConnectionString("WWDB");

//builder.Services.WestWindExtentionServices(options => options.UseSqlServer(connectionString));

builder.Services.AddDbContext<WestWindContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("WWDB")));

builder.Services.AddScoped<ProductServices>();
builder.Services.AddScoped<OrderServices>();
builder.Services.AddScoped<SupplierServices>();
builder.Services.AddScoped<CategoryServices>();



// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

builder.Services.AddMudServices(config =>
{
	config.SnackbarConfiguration.VisibleStateDuration = 10000;
	config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;
});

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
