using Api.Settings;
using Microsoft.Extensions.Options;
using NuGets.NuGets.HostedBase.Interface;

namespace Api.Hosted.Data.Abstract
{
    public class BaseServiceBusData : ISubscriptionClientData
    {
        private readonly IOptions<ServiceBusSettings> settings;

        public BaseServiceBusData(IOptions<ServiceBusSettings> settings)
        {
            this.settings = settings;
        }

        public string ConnectionString => settings.Value.ConnectionString;

        public string? StorageName { get; set; }

        public string SubscriptionName { get; set; } = default!;
    }
}
