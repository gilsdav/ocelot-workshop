using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Polly;

namespace PizzaGraphQL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CheckDatabase();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static async Task CheckDatabase() {
            var isDevelopment = string.Equals(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"), "development", StringComparison.InvariantCultureIgnoreCase);
            if (!isDevelopment) {
                var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();
                var startupPolicy = Policy
                    .Handle<SqlException>()
                    // .Handle<SqlException>(ex => {
                    //     return ex.Number == 0 || // network
                    //         ex.Number == 18456; // login
                    // })
                    .WaitAndRetryForeverAsync(
                        retryAttempt => TimeSpan.FromSeconds(5), // Math.Pow(2, retryAttempt)
                        (exception, timespan) =>
                        {
                            // Console.WriteLine($"Retry {exception}");
                            // Console.WriteLine($"Retry {timespan}");
                            Console.WriteLine("DB connection retry");
                        }
                    );
                await startupPolicy
                    .ExecuteAsync(
                        async ct => {
                            using (SqlConnection connection = new SqlConnection(config.GetConnectionString("sqlConString")))
                            {
                                connection.Open();
                                String sql = "SELECT 1";
                                using (SqlCommand command = new SqlCommand(sql, connection))
                                {
                                    await command.ExecuteReader().ReadAsync(CancellationToken.None);
                                    Console.WriteLine("DB ready");
                                }
                                
                            }
                        },
                        CancellationToken.None
                    );
            }
        }
    }
}
