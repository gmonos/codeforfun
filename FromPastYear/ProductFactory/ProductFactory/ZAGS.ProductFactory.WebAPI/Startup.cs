using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System.Net;
using Zags.Domain.Repositories;
using Zags.ProductFactory.Application.Managers;
using Zags.ProductFactory.Database;
using Zags.ProductFactory.Database.Repositories;
using Zags.ProductFactory.Database.Retrievers;
using Zags.ProductFactory.Domain;
using Zags.ProductFactory.Domain.Retrievers;
using Zags.ProductFactory.Domain.Validations;
using ZAGS.Domain.Specification;
using ZAGS.Domain.Validation;

namespace ZAGS.ProductFactory.WebAPI
{
    public class Startup
    {
        public static IConfiguration Configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                    .AddMvcOptions(o => o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter())); ;

            var connectionString = Configuration.GetConnectionString("ProductDbContext");
            services.AddDbContext<ProductDbContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddScoped<IProductRetriever, ProductRetriever>(x=> new ProductRetriever(connectionString));
            services.AddScoped<IValidator<Product>, ProductValidator>();
            services.AddScoped<ISpecificationDispacher<Product>, IsProductAttachedSpecifciation>();

          
            services.AddScoped<IProductManager, ProductManager>();

            services.AddSingleton(Configuration);

            services.AddSwaggerGen();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole();

            loggerFactory.AddDebug();

            loggerFactory.AddNLog();
 
            var logger = loggerFactory.CreateLogger<Startup>();

            app.UseExceptionHandler(options => {
                options.Run(
                            async context =>
                            {
                                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                context.Response.ContentType = "text/html";
                                var ex = context.Features.Get<IExceptionHandlerFeature>();
                                if (ex != null)
                                {
                                    string err = string.Empty;

                                    if (env.IsDevelopment())
                                        err = $"<h1>Error: {ex.Error.Message}</h1>{ex.Error.StackTrace }";
                                    else
                                        err = $"<h1>Technical issue</h1>";

                                    logger.LogCritical(1, ex.Error, ex.Error.StackTrace);

                                    await context.Response.WriteAsync(err).ConfigureAwait(false);
                                }
                            }); 
                });


           

            app.UseStatusCodePages();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, Models.ProductModel>()
                   .ForMember(dest => dest.Coverages, opt => opt.MapFrom(src => src.Coverages))
                   .ForMember(dest => dest.Packs, opt => opt.MapFrom(src => src.Packs)); 

                cfg.CreateMap<Models.ProductModel, Product>()
                   .ForMember(dest => dest.Coverages, opt => opt.MapFrom(src => src.Coverages))
                   .ForMember(dest => dest.Packs, opt => opt.MapFrom(src => src.Packs)); 

                cfg.CreateMap<Coverage, Models.CoverageModel>();
                cfg.CreateMap<Models.CoverageModel, Coverage>();

                cfg.CreateMap<Pack, Models.PackModel>();
                cfg.CreateMap<Models.PackModel, Pack>();
            });


            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUi();

            app.UseMvc();
        }
    }
}
