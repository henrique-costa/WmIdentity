using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WmIdentity.Models;
using WmIdentity.ViewModels;

namespace WmIdentity.Services
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private WmIdentityDbContext _context;
        private DbSet<T> _dbSet;


        public BaseRepository(WmIdentityDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Create(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

    

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public IEnumerable<T> Query(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }


        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }


        //-------------------------------------------PHOTOS
        //public void SavePhotos(T photo)
        //{
        //    _dbSet.Add(photo);
        //    _context.SaveChanges();
        //}

        //public void CreatePhoto(string path, int productId)
        //{
        //    CreatePhotoVM photo = new CreatePhotoVM
        //    {
        //        Path = path,
        //        ProductID = productId
        //    };
           
        //}
        //-------------------------------------------PHOTOS

    }
}
