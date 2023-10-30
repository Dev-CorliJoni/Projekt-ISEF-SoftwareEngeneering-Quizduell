using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Quixduell.Blazor.Areas.Identity;
using Quixduell.Blazor.Data;
using Quixduell.ServiceLayer;
using System.Diagnostics;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

//Get Connection String
var connectionString = builder.Configuration.GetConnectionString("SQL");

//Configure Entity Framework 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//Configure Identity Provider
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();



builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

//Add Service Layer
builder.Services.AddQuixServiceLayer();


var app = builder.Build();

//Init Database
using (var serviceScopce = app.Services.CreateScope())
{
    var Stopwatch = new Stopwatch();
    Stopwatch.Start();
    var AppDBContext = serviceScopce.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await AppDBContext.Database.MigrateAsync();
    app.Logger.LogInformation("Migration take: {Database Migration Time}", Stopwatch.Elapsed.ToString());

}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
