using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.MongoDb.Dtos
{
    public class TeamDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; }
    }
}
