using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WmIdentity.Models;

namespace WmIdentity.Services
{
    public interface IRepository<T>
    {
        //CRUD
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
        T GetSingle(Expression<Func<T, bool>> predicate);
        IEnumerable<T> Query(Expression<Func<T, bool>> predicate);

        //Photos
        //void SavePhotos(T photo);
        //void CreatePhoto(string path, int productId);

    }
}
