using System.ComponentModel.DataAnnotations;

namespace Api.Settings.Extensions
{
    internal static class DataAnnotationsExtensions
    {
        public static T ValidateDataAnnotations<T>(this T source)
            where T : class, new()
        {
            ValidateObject(source);
            return source;
        }

        private static void ValidateObject(object instance)
        {
            var validationContext = new ValidationContext(instance);
            Validator.ValidateObject(instance, validationContext);
        }
    }
}
