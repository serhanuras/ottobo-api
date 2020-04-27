using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Ottobo.Api.HostedServices
{

    public class HelloWorldHostedService : BackgroundService
    {
        private readonly ILogger<HelloWorldHostedService> _logger;
        public HelloWorldHostedService(ILogger<HelloWorldHostedService> logger)
        {
            this._logger = logger;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                this._logger.LogWarning("Hello World");
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}