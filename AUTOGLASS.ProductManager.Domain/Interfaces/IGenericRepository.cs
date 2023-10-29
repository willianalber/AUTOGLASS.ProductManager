using AUTOGLASS.ProductManager.Domain.Entities;

namespace AUTOGLASS.ProductManager.Domain.Interfaces
{
    public interface IGenericRepository<TEntity>
        where TEntity : EntityBase
    {
        Task<IList<TEntity>> GetAll();
        Task<TEntity> GetById(long id);
        Task Create(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(long id);
    }

}
