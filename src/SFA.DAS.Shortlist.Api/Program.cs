using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using SFA.DAS.Configuration.AzureTableStorage;
using SFA.DAS.Shortlist.Application.Data;

namespace SFA.DAS.Shortlist.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            NLogBuilder.ConfigureNLog("nlog.config");

            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.

            builder.Host.UseNLog();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ShortlistDataContext>((serviceProvider, options) => 
            {
                var connectionString = builder.Configuration["SqlDatabaseConnectionString"];
                var connection = new SqlConnection(connectionString);

                var environmentName = builder.Configuration["Environment"];
                if (environmentName != "LOCAL")
                {
                    const string AzureResource = "https://database.windows.net/";
                    var azureServiceTokenProvider = new AzureServiceTokenProvider();
                    var accessToken = azureServiceTokenProvider.GetAccessTokenAsync(AzureResource).Result;
                    connection.AccessToken = accessToken;
                }

                options.UseSqlServer(connection, o => o.CommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds));
            });

            builder.Services.AddHealthChecks().AddDbContextCheck<ShortlistDataContext>();

            builder.Host.ConfigureAppConfiguration((hostingContext, configurationBuilder) =>
            {
                configurationBuilder.AddAzureTableStorage(options =>
                {
                    var configuration = builder.Configuration;
                    options.ConfigurationKeys = configuration["ConfigNames"].Split(",");
                    options.StorageConnectionString = configuration["ConfigurationStorageConnectionString"];
                    options.EnvironmentName = configuration["Environment"];
                    options.PreFixConfigurationKeys = false;
                });
            });

            var app = builder.Build();


            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapHealthChecks("/ping");
            app.MapControllers();

            app.Run();
        }
    }
}