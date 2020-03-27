using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace SwaggerDuplicationIssueService
{
    public class TimeHostedService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
