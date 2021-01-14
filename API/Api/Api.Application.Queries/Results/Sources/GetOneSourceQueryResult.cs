using System;
using NuGets.NuGets.Contracts.Enums;

namespace Api.Application.Queries.Results.Sources
{
    public class GetOneSourceQueryResult
    {
        public Uri Uri { get; set; }

        public SourceConnectorType ConnectorType { get; set; }

        public SourceType SourceType { get; set; }
    }
}
