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
    public static class DependencyInjection
    {

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
        public static IServiceCollection AddQuixServiceLayer(this IServiceCollection services)
        {

            services.AddHostedService<DBStarter>();
            services.AddScoped<CategoryHandler>();
            services.AddScoped<StudysetHandler>();
            services.AddScoped<IssueManager>();
            return services;
        }

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

                //services.AddDbContext<AppDatabaseContext<User>>(option =>
                //{
                //    option.UseSqlServer(bBOptions.Value.ConnectionString);
                //}, ServiceLifetime.Transient);
            }

            services.AddScoped<DBConnectionFactory>();

            services.AddScoped<StudysetDataAccess>();
            services.AddScoped<CategoryDataAccess>();
            services.AddScoped<GlobalSearch>();
            services.AddScoped<InitSampleData>();
            services.AddScoped<GameManager>();
            services.AddScoped<StudysetView>();
            services.AddScoped<ContributorRequest>();


            return services;
        }
    }
}
