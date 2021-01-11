using System.ComponentModel.DataAnnotations;

namespace Api.Settings
{
    public class AppSettings
    {
        [Required] 
        public string MongoDbConnectionString { get; set; } = default!;
    }
}
