using System;
using System.IO;
using System.Text.Json;
using Microsoft.Azure.ServiceBus;

namespace SyncFunctionApp.Extensions
{
    //todo: Should be in a Nuget package.
    public static class ServiceBusMessageExtensions
    {
        public static Message ToBrokeredMessage<T>(this T message, string messageId = null)
        {
            using var memoryStream = new MemoryStream();
            var body = JsonSerializer.SerializeToUtf8Bytes<T>(message);
            var result = new Message(body)
            {
                ContentType = typeof(T).ToString()
            };

            if (!String.IsNullOrEmpty(messageId))
            {
                result.MessageId = messageId;
            }

            return result;
        }
    }
}
