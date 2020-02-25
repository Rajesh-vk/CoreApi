using System;
using System.Collections.Generic;
using System.Text;

namespace FSE_DataAccess.Interfaces
{
    public interface ICosmosRepositoryBase<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(string id);
        void Insert(TEntity obj);
        void Update(string id, TEntity obj);
        void Delete(string id);
        void Save();
    }
}
