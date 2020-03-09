using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PizzaGraphQL.Entities.Context;
using PizzaGraphQL.GraphQL;
using PizzaGraphQL.Repositories;
using PizzaGraphQL.Repositories.Implmentations;

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
            // TODO: set all singleton as scoped
            services.AddDbContext<ApplicationContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("sqlConString")), ServiceLifetime.Singleton);
            services.AddSingleton<IPizzaRepository, PizzaRepository>();
            services.AddSingleton<IToppingRepository, ToppingRepository>();
            #endregion

            services.AddMvc(options => {
                options.EnableEndpointRouting = false;
            });

            #region GraphQL
            // services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<PizzaQuery>();
            services.AddSingleton<PizzaMutation>();
            services.AddSingleton<PizzaSubscription>();
            services.AddSingleton<PizzaSchema>();
            services.AddSingleton<BobSchema>();
            // services.AddSingleton<ISchema, PizzaSchema>();
            services.AddGraphQL(o => { o.ExposeExceptions = false;  })
                .AddSystemTextJson(deserializerSettings => { }, serializerSettings => { })  
                .AddWebSockets() // For subscriptions
                .AddGraphTypes(ServiceLifetime.Singleton);
            #endregion
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // app.UseHttpsRedirection();
            app.UseWebSockets();
            app.UseGraphQLWebSockets<PizzaSchema>("/graphql");
            app.UseGraphQL<PizzaSchema>("/graphql");
            app.UseGraphQL<BobSchema>("/bob");
            // app.UseMvc();
            if (env.IsDevelopment())
            {
                app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions());
            }
        }
    }
}
