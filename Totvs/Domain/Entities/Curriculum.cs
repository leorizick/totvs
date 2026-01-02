using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Totvs.Domain.Entities
{
    public class Curriculum
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Updated { get; set; }

        public Candidate Candidate { get; set; }
        public int CandidateId { get; set; }
    }
}
