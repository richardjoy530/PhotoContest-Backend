using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Provider;
using Provider.Implementation;
using Server.Auth;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text;

namespace Server
{
    /// <summary>
    /// The <see cref="Startup"/> class configures services and the app's request pipeline.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        private static string XmlCommentsFilePath
        {
            get
            {
                var basePath = AppContext.BaseDirectory;
                var fileName = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }

        /// <summary>
        /// Initialises a <see cref="Startup"/> class
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Server", Version = "v1" });
                c.IncludeXmlComments(XmlCommentsFilePath);
            });

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Connection")));
            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(tokenOptions =>
            {
                var key = Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"]);
                tokenOptions.SaveToken = true;
                tokenOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true
                };
            });


            services.AddSingleton<IDbConnection>(db => new SqlConnection(Configuration.GetConnectionString("Connection")));
            services.AddSingleton<IReferenceIdMapper, ReferenceIdProvider>();
            services.AddSingleton<IReferenceIdProvider, ReferenceIdProvider>();
            services.AddSingleton<IPhotoEntryProvider, PhotoEntryProvider>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Server v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
