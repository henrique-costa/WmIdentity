using System;
using System.Collections.Generic;
using System.Text;
using WmIdentity.Models;

namespace WmIdentity.ViewModels
{
    public class ProductIndexData
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<SubCategory> SubCategories { get; set; }
        public int SelectedCategory { set; get; }




        #region INDEX GET DATA
        //Sorting
        public string NameSort { get; set; }
        //public string DateSort { get; set; }
        public string CatSort { get; set; }
        public string CatFilter { get; set; }
        public string searchString { get; set; }
        public string subCatFilter { get; set; }

        //[BindProperty(SupportsGet = true)]
        public List<string> AreChecked = new List<string>();

        public string CurrentFilter { get; set; }
        //public string CurrentCatFilter { get; set; }
        //public string CurrentSubCatFilter { get; set; }
        public string CurrentSort { get; set; }

        //public ProductIndexData Product { get; set; }
        public int? ProductId { get; set; }
        public Category Category { get; set; }

        #endregion
    }
}
