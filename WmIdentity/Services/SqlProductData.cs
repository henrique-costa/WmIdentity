using System.Collections.Generic;

using System.Linq;
using Microsoft.EntityFrameworkCore;
using WmIdentity.Models;
using WmIdentity.Services;

namespace WmIdentity.Services
{

    //parei aqui https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/read-related-data?view=aspnetcore-2.2&tabs=visual-studio
    public class SqlProductData : IProductData
    {
        private readonly WmIdentityDbContext db;

        public SqlProductData(WmIdentityDbContext Db)
        {
            db = Db;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Product Create(Product novoProduct)
        {
            db.Add(novoProduct);
            return novoProduct;
        }

        public Product Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
            {
                db.Products.Remove(product);
            }
            return product;
        }

        public Product GetById(int id)
        {
            return db.Products.Find(id);
        }

        public IEnumerable<Product> GetProductsByName(string name)
        {
            var query = from p in db.Products
                        where p.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                           orderby p.Name
                           select p;
            return query;

            //return dataContext.Orders
            //         .Include(order => order.OrderProducts)
            //         .ThenInclude(orderProducts => orderProducts.Product);
        }

        public IEnumerable<SubCategory> GetSubCategories()
        {
            //return db.SubCategories.ToList();
            var query = from sub in db.SubCategories
                        select sub;

            return query.ToList();
                
        }

        public Product Update(Product updatedProduct)
        {
            var entity = db.Products.Attach(updatedProduct);
            entity.State = EntityState.Modified;
            return updatedProduct;
        }
    }
}
