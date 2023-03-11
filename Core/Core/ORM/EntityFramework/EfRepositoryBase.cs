using Core.CommonModel;
using Core.CommonModel.Result;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.ORM.EntityFramework
{
  public class EfRepositoryBase<TEntity, TContext>
     : IRepositoryBase<TEntity>
     where TEntity : class, new()
     where TContext : DbContext, new()
  {
    public ServiceResult Add(TEntity Entity)
    {
      ServiceResult res = new ServiceResult();
      using (var context = new TContext())
      {
        try
        {
          context.Add(Entity);
          context.SaveChanges();
        }
        catch (Exception ex)
        {
          res.Status = false;
          res.Message = ex.Message;
        }
      }

      return res;
    }

    public ServiceResult Delete(TEntity Entity)
    {
      var res = new ServiceResult();
      using (var context = new TContext())
      {
        try
        {
          context.Remove(Entity);
          context.SaveChanges();
        }
        catch (Exception ex)
        {
          res.Status = false;
          res.Message = ex.Message;
        }
      }
      return res;
    }

    public IEnumerable<TEntity> ToList(Expression<Func<TEntity, bool>> Filter = null)
    {
      using (var context = new TContext())
      {
        var list = Filter == null ? context.Set<TEntity>().ToList() :
             context.Set<TEntity>().Where(Filter).ToList();
        return list;
      }
    }

    public TEntity GetByID(int id)
    {
      using (var context = new TContext())
      {
        return context.Set<TEntity>().Find(id);
      }
    }

    public ServiceResult<TEntity> GetSingle(Expression<Func<TEntity, bool>> Filter = null)
    {
      ServiceResult<TEntity> res = new ServiceResult<TEntity>();

      using (var context = new TContext())
      {
        try
        {
          res.Data = context.Set<TEntity>().FirstOrDefault(Filter);
        }
        catch (Exception ex)
        {
          res.Status = false;
          res.Message = ex.Message;
        }
      }

      return res;
    }

    public TEntity Find(Expression<Func<TEntity, bool>> filter)
    {
      using (var context = new TContext())
      {
        return context.Set<TEntity>().Where(filter).FirstOrDefault();
      }
    }

    public int AddBulk(IList<TEntity> entities)
    {
      using (var context = new TContext())
      {
        context.AddRange(entities);
        return context.SaveChanges();
      }
    }

    public ServiceResult<TEntity> GetByID(Expression<Func<TEntity, bool>> Filter = null)
    {
      throw new NotImplementedException();
    }

    public ServiceResult<TEntity> Update(TEntity Entity)
    {
      ServiceResult<TEntity> res = new ServiceResult<TEntity>();

      using (var context = new TContext())
      {
        try
        {
          var updatedEntity = context.Entry(Entity);
          updatedEntity.State = EntityState.Modified;
          context.SaveChanges();
          res.Data = Entity;
        }
        catch (Exception ex)
        {
          res.Status = false;
          res.Message = ex.Message;
        }
      }

      return res;
    }
  }
}
