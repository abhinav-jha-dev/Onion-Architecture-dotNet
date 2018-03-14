using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectOnion.Http.Dependencies;
using Swashbuckle.AspNetCore.Swagger;

namespace ProjectOnion.Http
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Getting all the configurations stated in the appsettings.
            var connectionString = Configuration.GetValue<string>("MongoConnection:ConnectionString");
            var databaseName = Configuration.GetValue<string>("MongoConnection:DatabaseName");
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Project Dependency Injection
            services.RegisterDependencies(connectionString, databaseName);

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Project Onion API",
                    Description = "A simple onion architecture based project api.",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Abhinav Jha", Email = "", Url = "https://www.linkedin.com/in/abhinav-jha-60269477/" },
                    License = new License { Name = "Use under MIT", Url = "https://github.com/abhinav2127/ProjectOnion" }
                });

                c.DocInclusionPredicate((docName, api) => api.RelativePath.Contains(docName));
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseMvc();
        }
    }
}
