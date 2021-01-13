using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace NuGets.NuGets.HostedBase.Abstracts.Base
{
    public abstract class HostedServiceBase : IHostedService, IDisposable
    {
        private Task executingTask;
        private CancellationTokenSource cts;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            executingTask = ExecuteAsync(cts.Token);
            return executingTask.IsCompleted ? executingTask : Task.CompletedTask;
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            if (executingTask == null)
            {
                return;
            }

            cts.Cancel();
            await Task.WhenAny(executingTask, Task.Delay(-1, cancellationToken));
            cancellationToken.ThrowIfCancellationRequested();
        }

        public virtual void Dispose()
        {
            executingTask?.Dispose();
            cts?.Dispose();
            GC.SuppressFinalize(this);
        }

        protected abstract Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
