using Core.CommonModel.Result;
using System.Linq.Expressions;

namespace Core.ORM
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        ServiceResult Add(TEntity Entity);
        ServiceResult Delete(TEntity Entity);
        ServiceResult<TEntity> Update(TEntity Entity);
        ServiceResult<TEntity> GetSingle(Expression<Func<TEntity, bool>> Filter = null);
        public TEntity Find(Expression<Func<TEntity, bool>> filter);
        IEnumerable<TEntity> ToList(Expression<Func<TEntity, bool>> Filter = null);
        TEntity GetByID(int id);
        ServiceResult<TEntity> GetByID(Expression<Func<TEntity, bool>> Filter = null);
        int AddBulk(IList<TEntity> entities);
    }
}
