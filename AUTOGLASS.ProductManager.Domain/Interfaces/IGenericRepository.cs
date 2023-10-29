using AUTOGLASS.ProductManager.Domain.Entities;

namespace AUTOGLASS.ProductManager.Domain.Interfaces
{
    public interface IGenericRepository<TEntity>
        where TEntity : EntityBase
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetById(long id);
        Task Create(TEntity entity);
        Task Update(long id, TEntity entity);
        Task Delete(long id);
    }

}
