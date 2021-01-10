using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Xml;
using Microsoft.Azure.ServiceBus;

namespace SyncFunctionApp.Extensions
{
    //todo: Should be in a Nuget package.
    public static class ServiceBusMessageExtensions
    {
        public static Message ToBrokeredMessage<T>(this T data, string uniqueId)
        {
            var ser = new DataContractSerializer(typeof(T));
            using var memoryStream = new MemoryStream();
            var binaryDictionaryWriter = XmlDictionaryWriter.CreateBinaryWriter(memoryStream);
            ser.WriteObject(binaryDictionaryWriter, data);
            binaryDictionaryWriter.Flush();
            var message = new Message(memoryStream.ToArray());
            message.ContentType = data.GetType().Name;

            if (!String.IsNullOrEmpty(uniqueId))
            {
                message.MessageId = uniqueId;
            }

            return message;
        }
    }
}
