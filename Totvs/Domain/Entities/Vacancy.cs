using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Totvs.Domain.Entities
{
    public class Vacancy
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> CandidateIds { get; set; } = new();
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;

        public Vacancy(string name, string description)
        {
            Validate(name, description);
            Name = name;
            Description = description;
        }

        public void Update(string name, string description)
        {
            Validate(name, description);
            Name = name;
            Description = description;
        }

        private static void Validate(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException("Name is required");

            if (string.IsNullOrWhiteSpace(description))
                throw new ValidationException("Invalid description");
        }
    }
}
