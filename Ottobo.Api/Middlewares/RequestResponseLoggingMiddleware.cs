using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Ottobo.Api.Middlewares
{

    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public RequestResponseLoggingMiddleware(RequestDelegate next,
                                                ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory
                      .CreateLogger<RequestResponseLoggingMiddleware>();
        }

        private static string ReadStreamInChunks(Stream stream)
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

        public async Task Invoke(HttpContext context)
        {

            using (var swapStream = new MemoryStream())
            {
                context.Request.EnableBuffering();

                await context.Request.Body.CopyToAsync(swapStream);

                string requestBody = ReadStreamInChunks(swapStream);

                _logger.LogInformation($"Http Request Information:{Environment.NewLine}" +
                       $"Schema:{context.Request.Scheme} " +
                       $"Host: {context.Request.Host} " +
                       $"Path: {context.Request.Path} " +
                       $"QueryString: {context.Request.QueryString} " +
                       $"Request Body: {requestBody}");


                context.Request.Body.Position = 0;
            }

            using (var swapStream = new MemoryStream())
            {
                var originalResponseBody = context.Response.Body;
                context.Response.Body = swapStream;

                await _next(context);

                swapStream.Seek(0, SeekOrigin.Begin);
                string responseBody = new StreamReader(swapStream).ReadToEnd();
                swapStream.Seek(0, SeekOrigin.Begin);



                await swapStream.CopyToAsync(originalResponseBody);
                context.Response.Body = originalResponseBody;

                _logger.LogInformation(responseBody);
            }
        }
    }
}