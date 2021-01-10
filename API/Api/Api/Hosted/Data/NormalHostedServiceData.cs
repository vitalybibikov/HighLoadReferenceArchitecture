using Api.Hosted.Data.Abstract;
using Api.Settings;
using Microsoft.Extensions.Options;

namespace Api.Hosted.Data
{
    public class NormalHostedServiceData : BaseServiceBusData
    {
        public NormalHostedServiceData(IOptions<ServiceBusSettings> settings)
            : base(settings)
        {
            StorageName = settings.Value.ServiceBusNormalCompetitionsTopicName;
            //todo: move to settings, when those hosted services be in a different projects;
            SubscriptionName = "NormalProcessing";
        }
    }
}
