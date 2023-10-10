using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PhotoContest.Implementation;

namespace PhotoContest.Web
{
    /// <summary>
    ///     The <see cref="Startup" /> class configures services and the app's request pipeline.
    /// </summary>
    public class Startup
    {
        private static string XmlCommentsFilePath
        {
            get
            {
                var basePath = AppContext.BaseDirectory;
                var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                return Path.Combine(basePath, fileName);
            }
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // TODO: Configure JsonConverter for StringToEnumConversion
            // TODO: Configure Swagger to use enum name instead of value
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PhotoContest PhotoContest.Web", Version = "v1" });
                c.IncludeXmlComments(XmlCommentsFilePath);
            });

            services.AddLogging();
            services.AddSingleton<ILogger, Logger<Startup>>();
            services.AddSingleton<IDatabase, Database>();
        
            services.ConfigureServices(true);
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PhotoContest.Web v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}