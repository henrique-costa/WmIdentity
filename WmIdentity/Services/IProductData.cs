using System.Collections.Generic;
using System.Text;
using WmIdentity.Models;

namespace WmIdentity.Services
{
    public interface IProductData
    {
        IEnumerable<Product> GetProductsByName(string name);
        Product GetById(int id);
        Product Update(Product updatedProduct);
        Product Create(Product novoProduct);
        Product Delete(int id);
        IEnumerable<SubCategory> GetSubCategories();
        int Commit();
    }
}
