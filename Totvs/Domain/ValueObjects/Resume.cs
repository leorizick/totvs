using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Totvs.Domain.ValueObjects
{
    public class Resume
    {
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public Resume(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ValidationException("Resume description is required");
            }

            Description = description;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
