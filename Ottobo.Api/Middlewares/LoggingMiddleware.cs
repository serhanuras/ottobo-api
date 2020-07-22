using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using Ottobo.Infrastructure.Data.IRepository;
using Ottobo.Infrastructure.Extensions;

namespace Ottobo.Api.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<LoggingMiddleware> _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public LoggingMiddleware(ILogger<LoggingMiddleware> logger, IConfiguration configuration, RequestDelegate next,
            IUnitOfWork unitOfWork)
        {
            _next = next;
            _logger = logger;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.Headers["StartDateTime"] =
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff");
            
            context.Request.EnableBuffering();
            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            context.Request.Body.Position = 0;

            var requestBodyText = ReadStreamInChunks(requestStream);

            context.Request.Headers["RequestBody"] = requestBodyText;


            var originalBodyStream = context.Response.Body;
            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;
            await _next(context);
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            await responseBody.CopyToAsync(originalBodyStream);
            
            context.Request.Headers["ResponseBody"] = responseBodyText;
            
            
            context.Request.Headers["EndDateTime"] =
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff");


            _logger.LogHttpContextToDb(context, _configuration, _unitOfWork);
        }

        private string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            stream.Seek(0, SeekOrigin.Begin);
            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);
            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;
            do
            {
                readChunkLength = reader.ReadBlock(readChunk,
                    0,
                    readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);

            return textWriter.ToString();
        }
    }
}