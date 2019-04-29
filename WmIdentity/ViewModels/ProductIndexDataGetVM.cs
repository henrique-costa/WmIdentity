using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WmIdentity.Models;

namespace WmIdentity.ViewModels
{
    public class ProductIndexDataGetVM
    {
        public IEnumerable<Product> Products   { get; set; }
        public IEnumerable<SubCategory> SubCategories { get; set; }

        public string CurrentFilter { get; set; }
        public string CatFilter { get; set; }
        public string NameSort { get; set; }
        public string CatSort { get; set; }
    }
}
