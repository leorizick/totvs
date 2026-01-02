using MongoDB.Driver;
using Totvs.Domain.Entities;
using Totvs.Infrastructure.Persistence;

namespace Totvs.Infrastructure.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {

        private readonly IMongoCollection<Candidate> _collection;

        public CandidateRepository(MongoContext context)
        {
            _collection = context.Candidates;
        }

        public async Task<Candidate?> CreateAsync(Candidate candidate)
        {
            await _collection.InsertOneAsync(candidate);
            return candidate;
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Candidate>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Candidate?> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Candidate>> GetManyByIdsAsync(IEnumerable<string> ids)
        {
            var filter = Builders<Candidate>.Filter.In(x => x.Id, ids);

            return await _collection.Find(filter).ToListAsync();
        }

        public async Task UpdateAsync(string id, Candidate candidate)
        {
            await _collection.ReplaceOneAsync(x => x.Id == id, candidate);
        }
    }
}
