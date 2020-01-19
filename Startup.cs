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
            services.AddDbContext<ApplicationContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("sqlConString")));
            services.AddScoped<IPizzaRepository, PizzaRepository>();
            services.AddScoped<IToppingRepository, ToppingRepository>();
            #endregion

            services.AddMvc(options => {
                options.EnableEndpointRouting = false;
            });

            #region GraphQL
            // services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddScoped<PizzaQuery>();
            services.AddScoped<PizzaMutation>();
            services.AddScoped<PizzaSchema>();
            services.AddScoped<BobSchema>();
            // services.AddSingleton<ISchema, PizzaSchema>();
            services.AddGraphQL(o => { o.ExposeExceptions = false;  }).AddGraphTypes(ServiceLifetime.Scoped);
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
            app.UseGraphQL<PizzaSchema>();
            app.UseGraphQL<BobSchema>("/bob");
            // app.UseMvc();
            if (env.IsDevelopment())
            {
                app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions());
            }
        }
    }
}
