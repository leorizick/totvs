using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using Totvs.Domain.ValueObjects;

namespace Totvs.Domain.Entities
{
    public class Candidate
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Resume? Resume { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;

        public Candidate(string name, string email)
        {
            Validate(name, email);
            Name = name;
            Email = email;
        }

        public void Update(string name, string email)
        {
            Validate(name, email);
            Name = name;
            Email = email;
            Updated = DateTime.Now;
        }

        private static void Validate(string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException("Name is required");

            if (!email.Contains("@"))
                throw new ValidationException("Invalid email");
        }

        public void UpdateResume(string description)
        {
            Resume = new Resume(description);
        }
    }
}
