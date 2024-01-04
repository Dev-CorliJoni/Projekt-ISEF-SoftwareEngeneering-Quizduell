using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.AzureAppServices;
using Quixduell.Blazor.Areas.Identity;
using Quixduell.Blazor.Services;
using Quixduell.ServiceLayer;
using Quixduell.ServiceLayer.DataAccessLayer;
using Quixduell.ServiceLayer.DataAccessLayer.Model;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Logging for Azure App Service
        builder.Logging.AddAzureWebAppDiagnostics();
        builder.Services.Configure<AzureFileLoggerOptions>(options =>
        {
            options.FileName = "azure-diagnostics-";
            options.FileSizeLimit = 50 * 1024; 
            options.RetainedFileCountLimit = 5;
        });

        //Get Connection String
        var connectionString = builder.Configuration.GetConnectionString("SQL");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString));
        }

        //Configure Entity Framework for Identity
        builder.Services.AddDbContext<AppDatabaseContext<User>>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        //Configure Identity Provider
        builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<AppDatabaseContext<User>>()
             .AddUserValidator<CustomEmailValidator>();

        //User Service
        builder.Services.AddScoped<UserService>();


        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<User>>();

        //AddDataLayer
        builder.Services.AddQuixDataLayer(option =>
        {
            option.ConnectionString = connectionString;
        });



      

        //Add Service Layer
        builder.Services.AddQuixServiceLayer();


        var app = builder.Build();

        //Isolated CSS
       app.UseStaticFiles();

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
    }
}