using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WmIdentity.Models;

namespace WmIdentity.ViewModels
{
    public class DisplayAllProductsVM
    {
        //public int Total { get; set; }
        public IEnumerable<Product> ProductList { get; set; }
        //public IEnumerable<Product> ProductListByCategory { get; set; }
        public int CategoryId { get; set; }

        //public IEnumerable<string>  PhotoPaths { get; set; }
        //public string searchList;
    }
}
