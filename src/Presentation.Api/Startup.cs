namespace PetProjects.MicroTransactionsApi.Presentation.Api
{
    using System.IO;
    using System.Reflection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.PlatformAbstractions;
    using PetProjects.MicroTransactionsApi.Infrastructure.Configuration;
    using PetProjects.MicroTransactionsApi.Presentation.Api.Swagger;
    using Swashbuckle.AspNetCore.Swagger;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private static string XmlCommentsFilePath
        {
            get
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = services.AddMvcCore().AddVersionedApiExplorer(o => o.GroupNameFormat = "'v'V");

            // Parts from .AddMvc() that we need (we don't need razor related services, etc.)
            builder.AddApiExplorer();
            builder.AddFormatterMappings();
            builder.AddDataAnnotations();
            builder.AddJsonFormatters();

            services.AddApiVersioning(o => o.ReportApiVersions = true);
            services.AddSwaggerGen(options =>
            {
                using (var provider = services.BuildServiceProvider())
                {
                    using (var scope = provider.CreateScope())
                    {
                        var versionDescriptionProvider = scope.ServiceProvider.GetRequiredService<IApiVersionDescriptionProvider>();

                        foreach (var description in versionDescriptionProvider.ApiVersionDescriptions)
                        {
                            options.SwaggerDoc(description.GroupName, Startup.CreateInfoForApiVersion(description));
                        }

                        // add a custom operation filter which sets default values
                        options.OperationFilter<SwaggerDefaultValues>();

                        // integrate xml comments
                        options.IncludeXmlComments(Startup.XmlCommentsFilePath);
                    }
                }
            });

            services.ConfigureDependencies(this.Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            //// TODO
            //// app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });
        }

        private static Info CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new Info
            {
                Title = $"MTS Transactions API {description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Description = "This is the Micro Transaction System Transactions API.",
                License = new License { Name = "MIT", Url = "https://opensource.org/licenses/MIT" }
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}