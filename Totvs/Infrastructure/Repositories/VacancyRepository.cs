using MongoDB.Driver;
using Totvs.Domain.Entities;
using Totvs.Infrastructure.Persistence;

namespace Totvs.Infrastructure.Repositories
{
    public class VacancyRepository : IVacancyRepository
    {

        private readonly IMongoCollection<Vacancy> _collection;

        public VacancyRepository(MongoContext context)
        {
            _collection = context.Vacancies;
        }

        public async Task<Vacancy?> CreateAsync(Vacancy vacancy)
        {
            await _collection.InsertOneAsync(vacancy);
            return vacancy;
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Vacancy>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Vacancy?> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, Vacancy vacancy)
        {
            await _collection.ReplaceOneAsync(x => x.Id == id, vacancy);
        }
    }
}
