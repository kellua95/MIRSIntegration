using MIRS.Web.Extensions;
using MIRS.Web.Middleware;
using MIRS.Application.Middleware;
using MIRS.Startup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAppServices(builder.Configuration);
var app = builder.Build();

app.UseMiddleware<WebExceptionMiddleware>();
app.UseMiddleware<TransactionMiddleware>();
app.UseStatusCodePagesWithReExecute("/StatusCode/{0}");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/StatusCode/500");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();
await DatabaseInitializer.InitializeAsync(app.Services);
app.Run();