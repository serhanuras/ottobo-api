using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Ottobo.Entities;
using Ottobo.Extensions;
using Ottobo.Api.Filters;
using Ottobo.HostedServices;
using Ottobo.Api.Middlewares;
using Ottobo.Data.Provider.IRepository;
using Ottobo.Data.Provider.PostgreSql;
using Ottobo.Data.Provider.Repository;
using Ottobo.Services;

namespace Ottobo.Api
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
            //adding filter globally...
            services.AddControllers(options => { options.Filters.Add(typeof(ExceptionFilter)); })
                .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    }
                )
                .AddXmlDataContractSerializerFormatters();


            services.AddCors(options =>
            {
                options.AddPolicy("AllowAPIRequestIO",
                    builder => builder.WithOrigins("https://www.apirequest.io").WithMethods("GET", "POST")
                        .AllowAnyHeader());
            });


            services.AddDbContextPool<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Ottobo.Api"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddResponseCaching();
            services.AddTransient<CustomActionFilter>();
            services.AddHostedService<HelloWorldHostedService>();
            services.AddAutoMapper(typeof(Startup));
            services.AddIdentity<ApplicationUser, IdentityRole<long>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<HashService>();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["jwt:key"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;

                // // Cookie settings
                // options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(150);
                // options.Cookies.ApplicationCookie.LoginPath = "/Account/Login";

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.AddHttpContextAccessor();


            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",

                    Title = "Ottobo.Api Web API",
                    Description = "This is a Web API for Ottobo.Api Clients",
                    License = new OpenApiLicense()
                    {
                        Name = "MIT"
                    },
                    Contact = new OpenApiContact()
                    {
                        Name = "Serhan URAS",
                        Email = "serhan.uras@ottobo.com.tr",
                        Url = new Uri("http://www.ottobo.com")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);

            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseCors();
                // This policy would be applied at the Web API level
                //app.UseCors(builder => 
                //builder.WithOrigins("https://www.apirequest.io").WithMethods("GET", "POST").AllowAnyHeader());
            }

            app.UseRequestResponseLogging<RequestResponseLoggingMiddleware>();


            app.UseSwagger();

            app.UseSwaggerUI(config => { config.SwaggerEndpoint("/swagger/v1/swagger.json", "Ottobo.Api API"); });


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseResponseCaching();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
