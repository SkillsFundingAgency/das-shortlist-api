using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog.Web;
using SFA.DAS.Api.Common.AppStart;
using SFA.DAS.Api.Common.Configuration;
using SFA.DAS.Api.Common.Infrastructure;
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

            var environmentName = builder.Configuration["Environment"];

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


            // Add services to the container.

            builder.Services.AddApplicationInsightsTelemetry();

            if (environmentName != "LOCAL")
            {
                var azureAdConfiguration = builder.Configuration
                    .GetSection("AzureAd")
                    .Get<AzureActiveDirectoryConfiguration>();

                var policies = new Dictionary<string, string>
                {
                    {PolicyNames.Default, "Default"}
                };

                builder.Services.AddAuthentication(azureAdConfiguration, policies);
            }

            builder.Host.UseNLog();
            builder.Services.AddApiVersioning(opt => 
            {
                opt.ApiVersionReader = new HeaderApiVersionReader("X-Version");
                opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            });
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options => 
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "ShortlistAPI", Version = "v1" });
            });

            builder.Services.AddDbContext<ShortlistDataContext>((serviceProvider, options) => 
            {
                var connectionString = builder.Configuration["SqlDatabaseConnectionString"];
                var connection = new SqlConnection(connectionString);

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


            var app = builder.Build();


            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment() || environmentName == "LOCAL")
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(options => 
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = HealthCheckResponseWriter.WriteJsonResponse
            });

            app.MapControllers();

            app.Run();
        }
    }
}