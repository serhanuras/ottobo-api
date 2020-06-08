using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Ottobo.Entities;
using Ottobo.Infrastructure.Data.IRepository;

namespace Ottobo.Infrastructure.Extensions
{
    public static class LoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging<T>(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<T>();
        }

        public static void LogHttpContextToDb(this ILogger logger,
            HttpContext context,
            IConfiguration configuration,
            IUnitOfWork unitOfWork)
        {
            try
            {
                if (context.Request.Path.ToString().Contains("api"))
                {
                    
                    DateTime startDateTime = context.Request.Headers.ContainsKey("StartDateTime")
                        ? DateTime.ParseExact(context.Request.Headers["StartDateTime"].ToString(),
                            "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture)
                        : DateTime.Now;
                    DateTime endDateTime = context.Request.Headers.ContainsKey("EndDateTime")
                        ? DateTime.ParseExact(context.Request.Headers["EndDateTime"].ToString(),
                            "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture)
                        : DateTime.Now;
                    
                    string requestBodyText =  !String.IsNullOrWhiteSpace(context.Request.Headers["RequestBody"]) ? context.Request.Headers["RequestBody"].ToString() : "";
                    string responseBodyText = !String.IsNullOrWhiteSpace(context.Request.Headers["ResponseBody"]) ? context.Request.Headers["ResponseBody"].ToString() : "";


                    var uriArray = context.Request.Path.ToString().Split("/");

                    var req = $"RequestUri :{context.Request.GetDisplayUrl()} " +
                              $"Request Body : {requestBodyText}";

                    var res = $"StatusCode :{context.Response.StatusCode} " +
                              $"Response Body : {responseBodyText}";


                    SensitiveDataInPlace sensitiveDataInPlace = SensitiveDataInPlace.None;
                    if (context.Request.Headers.ContainsKey("SensitiveDataInPlace"))
                    {
                        sensitiveDataInPlace =
                            (SensitiveDataInPlace) Enum.Parse(typeof(SensitiveDataInPlace),
                                context.Request.Headers["SensitiveDataInPlace"]);
                    }
                    else
                    {
                        var sensitiveDataConfiguration = configuration.GetHasSensitiveDataConfiguration();

                        if (sensitiveDataConfiguration.SensitiveDataList.Any(x =>
                            x.ApiPath.ToUpper() == context.Request.Path.Value.ToUpper()))
                        {
                            SensitiveData sensitiveData = sensitiveDataConfiguration.SensitiveDataList
                                .Where(x => x.ApiPath.ToUpper() == context.Request.Path.Value.ToUpper()).ToList()[0];

                            if (sensitiveData != null)
                            {
                                sensitiveDataInPlace = sensitiveData.SensitiveDataInPlace;
                            }
                        }
                    }

                    if (sensitiveDataInPlace != SensitiveDataInPlace.None)
                    {
                        if (sensitiveDataInPlace.Equals(SensitiveDataInPlace.Request))
                        {
                            req = "******";
                        }
                        else if (sensitiveDataInPlace.Equals(SensitiveDataInPlace.Response))
                        {
                            res = "******";
                        }
                        else
                        {
                            req = "******";
                            res = "******";
                        }
                    }

                    string logType = "Info";

                    if ((int) context.Response.StatusCode >= 500 && (int) context.Response.StatusCode < 600)
                    {
                        logType = "Error";
                    }

                    string actionBy = context.Request.Headers.ContainsKey("Auth-UserId")
                        ? "Anonymous"
                        : context.Request.Headers["Auth-UserId"].ToString();
                    
                    Task.Run(() =>
                    {
                       var _repository = unitOfWork.GetRepository<ApiLog>();

                       _repository.Add(new ApiLog()
                       {
                           MethodName = uriArray[2],
                           ControllerName = uriArray[1],
                           LogType = logType,
                           StartDate = startDateTime,
                           EndDate = endDateTime,
                           ActionBy = actionBy,
                           RequestBody = req,
                           ResponseBody = res,
                           CreatedOn = DateTime.UtcNow
                       });
                       
                       unitOfWork.Save();
                    });


                }
            }
            catch (Exception ex)
            {
                logger.LogInformation($"LoggingMiddlewareExtensions get exception Ex Message:{ex.Message} " +
                                      $"Stack Trace:{ex.StackTrace} ");
            }
        }
    }
    
    public enum SensitiveDataInPlace
    {
        None =0,
        Request = 1,
        Response =2,
        Both =3
    }
    
    public class SensitiveData
    {
        public string ApiPath { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public SensitiveDataInPlace SensitiveDataInPlace { get; set; }
    }
    
    public class SensitiveDataConfiguration
    {
        public List<SensitiveData> SensitiveDataList { get; set; }
    }
}