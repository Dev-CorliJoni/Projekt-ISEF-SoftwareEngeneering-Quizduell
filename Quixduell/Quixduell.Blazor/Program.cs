using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.AzureAppServices;
using Quixduell.Blazor;
using Quixduell.Blazor.Areas.Identity;
using Quixduell.Blazor.Services;
using Quixduell.ServiceLayer;
using Quixduell.ServiceLayer.DataAccessLayer;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.Services.MailSender.SendGrid;
using Quixduell.ServiceLayer.Services.MailSender.SMTP;

internal class Program
{
    private static void Main(string[] args)
    {
        var loggerFactory = LoggerFactory.Create(o =>
        {
            o.SetMinimumLevel(LogLevel.Information);
            o.AddConsole();
        });
        var logger = loggerFactory.CreateLogger<Program>();

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
            options.UseSqlServer(connectionString,option =>
            {
                option.CommandTimeout((int)TimeSpan.FromMinutes(2).TotalSeconds);
            }));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        //Configure Identity Provider
        builder.Services.AddDefaultIdentity<User>()
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



        var emailConfigName = Environment.GetEnvironmentVariable("EmailConfiguration");
        if (String.IsNullOrEmpty(emailConfigName)) 
        {
            logger.LogWarning("No Email Configuration found, you can set one with ENV EmailConfiguration = (SendGrid or SMTP)");
        }
        else
        {
            if (emailConfigName.ToLower() == "sendgrid")
            {
                logger.LogInformation("SendGrid detected, use SendGrid Options");
                builder.Services.AddSendGridEmailServices(builder.Configuration.GetSection(SendGridEmailConfiguration.Section));
            }
            else if (emailConfigName.ToLower() == "smtp")
            {
                logger.LogInformation("SMTP detected, use SMTP Options");
                builder.Services.AddSMTPEmailServices(builder.Configuration.GetSection(SMTPEmailConfiguration.Section));
            }
            else 
            {
                logger.LogWarning("No Email Configuration found, you can set one with ENV EmailConfiguration = (SendGrid or SMTP)");
            }
        }
        //For PW forget
        builder.Services.AddTransient<IEmailSender, EmailSender>();

        //Add Service Layer
        builder.Services.AddQuixServiceLayer();


        var app = builder.Build();

        //Isolated CSS
        StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);
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