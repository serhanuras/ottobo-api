using System;
using System.IO;
using System.Reflection;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Ottobo.Api.Dtos;
using Ottobo.Entities;
using Ottobo.Extensions;
using Ottobo.Api.Filters;
using Ottobo.HostedServices;
using Ottobo.Api.Middlewares;
using Ottobo.Infrastructure.Data.IRepository;
using Ottobo.Infrastructure.Data.PostgreSql;
using Ottobo.Infrastructure.Data.Repository;
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
                        options.SerializerSettings.ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy()
                        };
                    }
                )
                .AddXmlDataContractSerializerFormatters();


            #if DEBUG
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAPIRequestIO",
                    builder => builder.WithOrigins("https://www.apirequest.io").WithMethods("GET", "POST")
                        .AllowAnyHeader());
            });
            #endif

            
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
            
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<ApplicationLogs>>();
            services.AddSingleton(typeof(ILogger), logger);

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
            services.AddScoped<MasterDataService>();
            services.AddScoped<OrderDetailService>();
            services.AddScoped<OrderService>();
            services.AddScoped<OrderTypeService>();
            services.AddScoped<PurchaseTypeService>();
            services.AddScoped<StockService>();
            services.AddScoped<StockTypeService>();
            services.AddScoped<LocationService>();
            
            
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
                        Email = "serhan.uras@ottobo.com",
                        Url = new Uri("http://www.ottobo.com")
                    }
                });
                
                config.SchemaFilter<SnakeCaseSchemaFilter>();
                
                
                
                

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

            #if DEBUG
            //app.UseRequestResponseLogging<RequestResponseLoggingMiddleware>();
            #endif

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