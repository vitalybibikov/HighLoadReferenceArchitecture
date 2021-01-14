using System;

namespace LiveSyncFunctionApp.Extensions
{
    //todo: Should be in a Nuget package.
    public static class UriExtensions
    {
        public static string ToBase64OrNull(this Uri data)
        {
            if (data != null)
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(data.ToString());
                return System.Convert.ToBase64String(plainTextBytes);
            }

            return null;
        }
    }
}