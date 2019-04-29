using System;
using System.Collections.Generic;
using System.Text;
using WmIdentity.Models;

namespace WmIdentity.Models
{
    public class ProductSubcategory
    {

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}
