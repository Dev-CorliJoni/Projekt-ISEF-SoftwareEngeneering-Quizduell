using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quixduell.ServiceLayer.DataAccessLayer;
using Quixduell.ServiceLayer.DataAccessLayer.Model;
using Quixduell.ServiceLayer.DataAccessLayer.Options;
using Quixduell.ServiceLayer.DataAccessLayer.Repository;
using Quixduell.ServiceLayer.DataAccessLayer.Repository.Implementation;
using Quixduell.ServiceLayer.ServiceLayer;
using Quixduell.ServiceLayer.ServiceLayer.SharedFunctionality;
using Quixduell.ServiceLayer.Services.HostedServices;
using Quixduell.ServiceLayer.Services.MailSender;
using Quixduell.ServiceLayer.Services.MailSender.SendGrid;
using Quixduell.ServiceLayer.Services.MailSender.SMTP;
using SendGrid.Extensions.DependencyInjection;

namespace Quixduell.ServiceLayer
{
    /// <summary>
    /// Represents a class for configuring dependency injection.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds SendGrid email services to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="sendGridConfiguration">The SendGrid configuration.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddSendGridEmailServices(this IServiceCollection services, IConfiguration sendGridConfiguration)
        {
            services.Configure<SendGridEmailConfiguration>(sendGridConfiguration);
            using (var scope = services.BuildServiceProvider())
            {
                var option = scope.GetService<IOptions<SendGridEmailConfiguration>>();
                if (option.Value.CheckValues())
                {
                    services.AddSendGrid(options =>
                    {
                        options.ApiKey = option.Value.ApiKey;
                    });
                    services.AddTransient<IMailSender, MailSenderSendGrid>();
                }
            }
            return services;
        }

        /// <summary>
        /// Adds dummy email services to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddDummyEmailServices(this IServiceCollection services)
        {
            services.AddTransient<IMailSender, DummyMailSender>();
            return services;
        }

        /// <summary>
        /// Adds SMTP email services to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="SmtpOptions">The SMTP configuration options.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddSMTPEmailServices(this IServiceCollection services, IConfiguration SmtpOptions)
        {
            services.Configure<SMTPEmailConfiguration>(SmtpOptions);
            using (var scope = services.BuildServiceProvider())
            {
                var option = scope.GetService<IOptions<SMTPEmailConfiguration>>();
                if (option.Value.CheckValues())
                {
                    services.AddTransient<IMailSender, MailSenderSMTP>();
                }
            }
            return services;
        }

        /// <summary>
        /// Adds services for the Quix service layer.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddQuixServiceLayer(this IServiceCollection services)
        {
            services.AddHostedService<DBStarter>();
            services.AddScoped<CategoryHandler>();
            services.AddScoped<StudysetHandler>();
            services.AddScoped<IssueManager>();
            return services;
        }

        /// <summary>
        /// Adds services for the Quix data layer.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="DataAccessOption">The data access options.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddQuixDataLayer(this IServiceCollection services, Action<DataAccessOptions> DataAccessOption)
        {
            services.Configure<DataAccessOptions>(DataAccessOption);
            using (var scope = services.BuildServiceProvider())
            {
                var bBOptions = scope.GetRequiredService<IOptions<DataAccessOptions>>();
                services.AddDbContext<AppDatabaseContext<User>>(option =>
                {
                    option.UseSqlServer(bBOptions.Value.ConnectionString);
                });
            }
            services.AddScoped<DBConnectionFactory>();
            services.AddScoped<StudysetDataAccess>();
            services.AddScoped<CategoryDataAccess>();
            services.AddScoped<GlobalSearch>();
            services.AddScoped<GameManager>();
            services.AddScoped<StudysetView>();
            services.AddScoped<ContributorRequest>();
            return services;
        }
    }
}
