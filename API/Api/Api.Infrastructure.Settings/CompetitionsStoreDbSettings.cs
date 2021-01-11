using System.ComponentModel.DataAnnotations;

namespace Api.Infrastructure.Settings
{
    public class CompetitionsStoreDbSettings
    {
        [Required]
        public string CollectionName { get; set; } = default!;

        [Required]
        public string DatabaseName { get; set; } = default!;
    }
}
