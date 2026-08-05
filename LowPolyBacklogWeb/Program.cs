using LowPolyBacklogWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddHttpClient("LowPolyBacklogApi", client =>
{
    var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"]
                     ?? throw new InvalidOperationException("The API base URL is not configured.");

    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddScoped<IGameApiService, GameApiService>();
builder.Services.AddScoped<IIgdbApiService, IgdbApiService>();
builder.Services.AddScoped<IBacklogApiService, BacklogApiService>();
builder.Services.AddScoped<IDashboardApiService, DashboardApiService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapGet("/health", () => Results.Ok("healthy"));
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
