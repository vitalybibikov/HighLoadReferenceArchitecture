using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using Microsoft.Azure.ServiceBus;

namespace Api.Infrastructure.Notification.Handlers.Extensions
{
    //todo: Should be in a Nuget package.
    public static class ServiceBusMessageExtensions
    {
        public static Message ToBrokeredMessage<T>(this T data)
        {
            var ser = new DataContractSerializer(typeof(T));
            using var memoryStream = new MemoryStream();
            var binaryDictionaryWriter = XmlDictionaryWriter.CreateBinaryWriter(memoryStream);
            ser.WriteObject(binaryDictionaryWriter, data);
            binaryDictionaryWriter.Flush();
            var message = new Message(memoryStream.ToArray())
            {
                ContentType = data?.GetType().Name
            };

            return message;
        }
    }
}