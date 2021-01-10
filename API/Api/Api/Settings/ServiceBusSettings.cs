using System.ComponentModel.DataAnnotations;

namespace Api.Settings
{
    public class ServiceBusSettings
    {
        [Required] 
        public string ConnectionString { get; set; } = default!;

        [Required]
        public string ServiceBusNormalCompetitionsTopicName { get; set; } = default!;

        [Required]
        public string ServiceBusLiveCompetitionsTopicName { get; set; } = default!;

        [Required]
        public string ServiceBusLiveQueueName { get; set; } = default!;

        [Required]
        public string ServiceBusQueueName { get; set; } = default!;

        public string SubscriptionName { get; set; } = default!;
    }
}
