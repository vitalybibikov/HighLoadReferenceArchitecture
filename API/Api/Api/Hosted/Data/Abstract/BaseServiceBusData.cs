using Api.Settings;
using Core.NuGets.HostedBase.Interface;
using Microsoft.Extensions.Options;

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
