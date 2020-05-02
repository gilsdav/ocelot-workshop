using System.Linq;
using System.Security.Claims;
using GraphQL.Server;
using GraphQL.Server.Ui.GraphiQL;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PizzaGraphQL.Entities.Context;
using PizzaGraphQL.GraphQL.PizzaGraphQL;
using PizzaGraphQL.GraphQL.SecurityGraphQL;
using PizzaGraphQL.Repositories;
using PizzaGraphQL.Repositories.Implmentations;
using PizzaGraphQL.Services;
using PizzaGraphQL.Services.Implementations;

namespace PizzaGraphQL
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            #region ApplicativeServer
            // kestrel
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            // IIS
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            #endregion

            #region Entity
            services.AddDbContext<ApplicationContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("sqlConString")));
            services.AddSingleton<IEventsService, EventsService>();
            services.AddScoped<IPizzaRepository, PizzaRepository>();
            services.AddScoped<IToppingRepository, ToppingRepository>();
            #endregion

            // services.AddMvc(options => {
            //     options.EnableEndpointRouting = false;
            // });

            services.AddCors(o => o.AddPolicy("MainPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:5000","http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            }));

            #region authentication
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(o =>
            {
                o.Cookie.Name = "graph-auth";
                o.Cookie.SameSite = SameSiteMode.Unspecified;
                o.Cookie.HttpOnly = false;
            });
            services.AddHttpContextAccessor();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            #endregion

            #region GraphQL
            services.AddScoped<PizzaQuery>();
            services.AddScoped<PizzaMutation>();
            services.AddScoped<PizzaSubscription>();
            services.AddScoped<PizzaSchema>();
            services.AddScoped<SecuritySchema>();

            services.AddGraphQL(o => { o.ExposeExceptions = false;  })
                .AddSystemTextJson(deserializerSettings => { }, serializerSettings => { })  
                .AddWebSockets() // For subscriptions
                .AddGraphTypes(ServiceLifetime.Scoped)
                .AddGraphQLAuthorization(options => {
                    options.AddPolicy("LoggedIn", p => p.RequireAuthenticatedUser());
                    options.AddPolicy("Bob", p => p.RequireClaim(ClaimTypes.Name, "Bob"));
                });
            #endregion
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable Cors
            app.UseCors("MainPolicy");
            // app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseWebSockets();
            app.UseGraphQLWebSockets<PizzaSchema>("/graphql");
            app.UseGraphQL<PizzaSchema>("/graphql");
            app.UseGraphQL<SecuritySchema>("/auth");
            // app.UseMvc();
            // if (env.IsDevelopment())
            // {
                app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
                app.UseGraphiQLServer(new GraphiQLOptions
                {
                    Path = "/ui/graphiql/pizza",
                    GraphQLEndPoint = "/graphql"
                });
                app.UseGraphiQLServer(new GraphiQLOptions
                {
                    Path = "/ui/graphiql/security",
                    GraphQLEndPoint = "/auth"
                });
            // }
            this.ApplyMigrations(db);
        }

        public void ApplyMigrations(ApplicationContext context) {
            if (context.Database.GetPendingMigrations().Any()) {
                context.Database.Migrate();
            }
        }
    }
}
