using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ottobo.Api.Services
{

    public class WriteToFileHostedService : IHostedService
    {
        private readonly IWebHostEnvironment _env;
        private readonly string filename = "File1.txt";

         private Timer _timer;

        private readonly ILogger<WriteToFileHostedService> _logger;
        public WriteToFileHostedService(IWebHostEnvironment env, ILogger<WriteToFileHostedService> logger)
        {
            this._env = env;
            this._logger = logger;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {

            WriteToFile("WriteToFileHostedService is started");

            _timer = new Timer(HelloWorld, null, 0, 10000);

            this._logger.LogInformation("WriteToFileHostedService is started");

            return Task.CompletedTask;
        }

        void HelloWorld(object state)
        {
           this._logger.LogDebug($@"Hello World!-  {_env.ContentRootPath}/wwwroot/{filename}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            WriteToFile("WriteToFileHostedService is stopped");

            this._logger.LogInformation("WriteToFileHostedService is stopped");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void WriteToFile(string message)
        {
            var path = $@"{_env.ContentRootPath}/wwwroot/{filename}";

            using (var writer = new StreamWriter(path, append: true))
            {
                writer.WriteLine(message);
            };

        }
    }

}