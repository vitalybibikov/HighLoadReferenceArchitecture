using System;
using MediatR;
using NuGets.NuGets.Contracts.Enums;
using NuGets.NuGets.Dtos.Enums;

namespace Api.Application.Notifications
{
    public class SendNormalSourceNotification: INotification
    {
        public Uri Uri { get; set; }

        public DateTime When { get; set; }

        public SourceConnectorType ConnectorType { get; set; }

        public SourceType SourceType { get; set; }

        public SportType SportType { get; set; }
    }
}
