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
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        options.SerializerSettings.ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy()
                        };
                    }
                )
                .AddXmlDataContractSerializerFormatters();


            
            
           services.AddCors(options =>
           {
               options.AddPolicy("AllowSpecificOrigins",
                   builder => builder.WithOrigins("http://www.apirequest.io","http://localhost:3000","http://admin.ottobotest.com")
                       .AllowAnyHeader()
                       .AllowAnyMethod());
               
              
           });
           
        
            
           
          /*
           services.AddCors(options =>
           {
               options.AddPolicy("AllowAll", builder =>
               {
                   builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowAnyOrigin()
                       .AllowCredentials()
                       .WithExposedHeaders("Location"); // params string[]
               });
           });
           */
          

            
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
            
            
            services.AddScoped<MasterDataService>(conf =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var logger = serviceProvider.GetService<ILogger<MasterDataService>>();
                var unitOfWork = serviceProvider.GetService<IUnitOfWork>();
               
                return new MasterDataService(logger, unitOfWork, "PurchaseType");
            });
            
            services.AddScoped<OrderDetailService>(conf =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var logger = serviceProvider.GetService<ILogger<OrderDetailService>>();
                var unitOfWork = serviceProvider.GetService<IUnitOfWork>();
               
                return new OrderDetailService(logger, unitOfWork, "Order,Stock,RobotTask,Stock.MasterData,Stock.Location");
            });
            
            services.AddScoped<OrderService>(conf =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var logger = serviceProvider.GetService<ILogger<OrderService>>();
                var unitOfWork = serviceProvider.GetService<IUnitOfWork>();
               
                return new OrderService(logger, unitOfWork, "OrderType,OrderDetailList.Stock,OrderDetailList.Stock.MasterData,OrderDetailList.Stock.Location,OrderDetailList.Stock.StockType");
            });
            
            services.AddScoped<OrderTypeService>(conf =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var logger = serviceProvider.GetService<ILogger<OrderTypeService>>();
                var unitOfWork = serviceProvider.GetService<IUnitOfWork>();
               
                return new OrderTypeService(logger, unitOfWork, "");
            });
            
            services.AddScoped<PurchaseTypeService>(conf =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var logger = serviceProvider.GetService<ILogger<PurchaseTypeService>>();
                var unitOfWork = serviceProvider.GetService<IUnitOfWork>();
               
                return new PurchaseTypeService(logger, unitOfWork, "");
            });
            
            services.AddScoped<StockService>(conf =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var logger = serviceProvider.GetService<ILogger<StockService>>();
                var unitOfWork = serviceProvider.GetService<IUnitOfWork>();
               
                return new StockService(logger, unitOfWork,  "MasterData,StockType,Location");
            });
            
            services.AddScoped<StockTypeService>(conf =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var logger = serviceProvider.GetService<ILogger<StockTypeService>>();
                var unitOfWork = serviceProvider.GetService<IUnitOfWork>();
               
                return new StockTypeService(logger, unitOfWork,  "");
            });
            
            services.AddScoped<LocationService>(conf =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var logger = serviceProvider.GetService<ILogger<LocationService>>();
                var unitOfWork = serviceProvider.GetService<IUnitOfWork>();
                var orderDetailService = serviceProvider.GetService<OrderDetailService>();
               
                return new LocationService(logger, unitOfWork,  orderDetailService, "");
            });
            
            services.AddScoped<RobotService>(conf =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var logger = serviceProvider.GetService<ILogger<RobotService>>();
                var unitOfWork = serviceProvider.GetService<IUnitOfWork>();
               
                return new RobotService(logger, unitOfWork,  "");
            });
            
           
            services.AddScoped<RobotTaskService>(conf =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var logger = serviceProvider.GetService<ILogger<RobotTaskService>>();
                var unitOfWork = serviceProvider.GetService<IUnitOfWork>();
               
                return new RobotTaskService(logger, unitOfWork,  "");
            });


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
          

            #if DEBUG
            //app.UseRequestResponseLogging<RequestResponseLoggingMiddleware>();
            #endif

            app.UseSwagger();
            app.UseSwaggerUI(config => { config.SwaggerEndpoint("/swagger/v1/swagger.json", "Ottobo.Api API"); });
            app.UseMiddleware<LoggingMiddleware>();
            //app.UseMiddleware<OptionsMiddleware>();
            
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseResponseCaching();
            app.UseAuthorization();
            app.UseCors("AllowSpecificOrigins");
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}