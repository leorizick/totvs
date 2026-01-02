using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Totvs.Domain.Entities;
using Totvs.Infrastructure.Settings;

namespace Totvs.Infrastructure.Persistence
{
    public class MongoContext
    {
        public IMongoDatabase Database { get; }

        public IMongoCollection<Candidate> Candidates { get; }
        public IMongoCollection<Vacancy> Vacancies { get; }
        public MongoContext(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            Database = client.GetDatabase(settings.Value.DatabaseName);

            Candidates = Database.GetCollection<Candidate>("Candidates");
            Vacancies = Database.GetCollection<Vacancy>("Vacancies");
        }
    }
}
