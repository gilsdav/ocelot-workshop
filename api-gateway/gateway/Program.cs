using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

using System;
using IdentityModel.AspNetCore.OAuth2Introspection;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using gateway.Aggregators;
using Ocelot.Cache.CacheManager;
using Ocelot.Administration;

namespace gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new WebHostBuilder()
            .UseKestrel()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config
                    .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                    .AddJsonFile("appsettings.json", true, true)
                    .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                    .AddJsonFile($"config/{Environment.GetEnvironmentVariable("OCELOT_SCOPE")}.ocelot.json")
                    .AddEnvironmentVariables();
            })
            .ConfigureServices(s =>
            {
                var authenticationProviderKey = "IdentityServerKey";
                Action<IdentityServerAuthenticationOptions> options = o =>
                {
                    o.Authority = "http://identity-server:5002";
                    o.ApiName = "public-gateway";
                    o.SupportedTokens = SupportedTokens.Both;
                    o.RequireHttpsMetadata = false;
                    o.ApiSecret = "secret";
                };
                s.AddAuthentication()
                    .AddIdentityServerAuthentication(authenticationProviderKey, options);
                s.AddOcelot()
                    .AddCacheManager(x =>
                     {
                         x.WithDictionaryHandle();
                     })
                    .AddSingletonDefinedAggregator<MyAggregator>()
                    .AddAdministration("/administration", options);
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                //add your logging
                logging.AddConsole();
            })
            .UseIISIntegration()
            .Configure(app =>
            {
                app.UseCookiePolicy();
                app.UseWebSockets();
                app.UseOcelot().Wait();
            })
            .Build()
            .Run();
        }
    }
}