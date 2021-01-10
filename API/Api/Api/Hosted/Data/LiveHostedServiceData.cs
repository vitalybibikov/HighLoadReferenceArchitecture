using Api.Hosted.Data.Abstract;
using Api.Settings;
using Microsoft.Extensions.Options;

namespace Api.Hosted.Data
{
    public class LiveHostedServiceData : BaseServiceBusData
    {
        public LiveHostedServiceData(IOptions<ServiceBusSettings> settings)
            : base(settings)
        {
            StorageName = settings.Value.ServiceBusLiveCompetitionsTopicName;
            //todo: move to settings, when those hosted services be in a different projects;
            SubscriptionName = "LiveProcessing";
        }
    }
}
