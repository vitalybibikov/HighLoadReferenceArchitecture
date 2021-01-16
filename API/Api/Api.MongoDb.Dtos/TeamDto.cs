using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.MongoDb.Dtos
{
    public class TeamDto
    {
        public string Name { get; set; }
    }
}
