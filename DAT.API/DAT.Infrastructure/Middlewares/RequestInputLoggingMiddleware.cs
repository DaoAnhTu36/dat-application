using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DAT.Infrastructure.Middlewares
{
    public class RequestInputLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestInputLoggingMiddleware> _logger;

        public RequestInputLoggingMiddleware(RequestDelegate next, ILogger<RequestInputLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var injectedRequestStream = new MemoryStream();

            try
            {
                var bodyLog = string.Empty;
                using (var bodyReader = new StreamReader(context.Request.Body))
                {
                    var bodyAsText = bodyReader.ReadToEnd();
                    if (string.IsNullOrWhiteSpace(bodyAsText) == false)
                    {
                        bodyLog += bodyAsText;
                    }

                    var bytesToWrite = Encoding.UTF8.GetBytes(bodyAsText);
                    injectedRequestStream.Write(bytesToWrite, 0, bytesToWrite.Length);
                    injectedRequestStream.Seek(0, SeekOrigin.Begin);
                    context.Request.Body = injectedRequestStream;
                }
                _logger.LogInformation($"REQUEST BODY: \n{bodyLog}");
                _logger.LogInformation($"REQUEST PATH: {context.Request.Path}");
                _logger.LogInformation($"REQUEST HEADERS: \n{string.Join("\n", context.Request.Headers.Select(t => $"{t.Key}: {SensitiveDataRedacted.HeaderRedactedString(t.Key, t.Value)}"))}");
                _logger.LogInformation($"REQUEST QUERIES: \n{string.Join("\n", context.Request.Query.Select(t => $"{t.Key}: {t.Value}"))}");
                await _next.Invoke(context);
            }
            finally
            {
                injectedRequestStream.Dispose();
            }
        }
    }
}