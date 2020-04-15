using FluentValidation;
using FoxHound.App.Data;
using FoxHound.App.Utility;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;

namespace FoxHound.Web
{
    public class Startup
    {
        // Application assembly used for dependency resolution of MediatR and FluentValidation.
        private const string ApplicationAssemblyName = "FoxHound.App";

        private const string LocalCorsPolicy = "LocalCorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(LocalCorsPolicy, builder =>
                {
                    builder.WithOrigins("http://localhost:1234");
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            services.AddApplicationInsightsTelemetry();
            services.AddControllers();

            string connectionString = Configuration.GetConnectionString("FoxHoundConnection");
            services.AddDbContext<FoxHoundData>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IFoxHoundData, FoxHoundData>();

            services.AddValidatorsFromAssembly(Assembly.Load(ApplicationAssemblyName));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddMediatR(Assembly.Load(ApplicationAssemblyName));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FoxHound API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Cannot be marked static as it is part of the .NET Core startup")]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors(LocalCorsPolicy);
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseHttpsRedirection();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FoxHound API V1");
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (!env.IsDevelopment())
            {
                // This will allow html5 client routing to continue to work when refreshing/bookmarking a page that is a client route
                // https://weblog.west-wind.com/posts/2017/aug/07/handling-html5-client-route-fallbacks-in-aspnet-core
                app.Run(async (context) =>
                {
                    context.Response.ContentType = "text/html";
                    await context.Response.SendFileAsync(Path.Combine(env.WebRootPath, "index.html"));
                });
            }
        }
    }
}