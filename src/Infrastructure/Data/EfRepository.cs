using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class EfRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _dbContext;
        public EfRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);//normalde referans projede hilo kullanıldığı için addasync ile veri tabanında haberleşmesi gerekiyor. Biz Hilo kullanmadığımız için async olmasına gerek yok.
               //https://www.talkingdotnet.com/use-hilo-to-generate-keys-with-entity-framework-core/
               //https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-implementation-entity-framework-core
                //_dbContext.Add(entity);

            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task DeleteAsync(T entity) //async interface de yapamıyoruz
        {
            _dbContext.Set<T>().Remove(entity);//set<T> db.urunler gibi
            //_dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> FirstOrDefaultAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();// ef core kütüphanesindeki
        }

        public async Task<List<T>> ListAsync(ISpecification<T> spec)
        {
            return await _dbContext.Set<T>().ToListAsync(spec);//ardalis kütüphanesi
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            //_dbContext.Update(entity); eş değer yukarıdaki ile
            await _dbContext.SaveChangesAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec) //ardalis kütüphanesini kullanırken metodlaştırdık
        {
            var evaluator = new SpecificationEvaluator();
            return evaluator.GetQuery<T>(_dbContext.Set<T>(), spec);
        }
    }
}
