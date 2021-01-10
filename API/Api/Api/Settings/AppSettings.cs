using System.ComponentModel.DataAnnotations;

namespace Api.Settings
{
    public class AppSettings
    {
        [Required] 
        public string ConnectionString { get; set; } = default!;
    }
}
