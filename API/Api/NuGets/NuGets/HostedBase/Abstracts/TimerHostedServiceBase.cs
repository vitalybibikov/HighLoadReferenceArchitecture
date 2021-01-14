using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace NuGets.NuGets.HostedBase.Abstracts
{
    public abstract class TimerHostedServiceBase : IHostedService, IDisposable
    {
        private readonly TimeSpan timeSpan;
        private Timer timer = default!;
        private CancellationTokenSource? cts;

        protected TimerHostedServiceBase(TimeSpan timeSpan)
        {
            this.timeSpan = timeSpan;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            timer = new Timer(
                OnTimer,
                null,
                timeSpan,
                timeSpan);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Change(Timeout.Infinite, 0);
            cts?.Cancel();
            cancellationToken.ThrowIfCancellationRequested();

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected abstract Task OnTimerAsync(object? state, CancellationToken cancellationToken);

        protected virtual void Dispose(bool disposing)
        {
            timer.Dispose();
            cts?.Dispose();
        }

        private async void OnTimer(object? state)
        {
            await OnTimerAsync(state, cts?.Token ?? CancellationToken.None);
        }
    }
}
