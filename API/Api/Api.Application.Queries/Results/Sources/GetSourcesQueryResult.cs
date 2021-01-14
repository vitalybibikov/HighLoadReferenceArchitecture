using System.Collections.Generic;

namespace Api.Application.Queries.Results.Sources
{
    public class GetSourcesQueryResult
    {
        public List<GetOneSourceQueryResult> Sources = new List<GetOneSourceQueryResult>();
    }
}
