using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WmIdentity.Models;
using WmIdentity.Services;
using WmIdentity.ViewModels;

namespace WmIdentity.Pages.ProductsVM
{
    public class IndexModel : PageModel
    {

        private readonly WmIdentityDbContext _context;
        private readonly IHtmlHelper htmlHelper;

        //Sorting
        public string NameSort { get; set; }
        //public string DateSort { get; set; }
        public string CatSort { get; set; }
        public string CatFilter { get; set; }

        public string SubCatFilter { get; set; }

        //[BindProperty(SupportsGet = true)]
        public List<string> AreChecked = new List<string>();


        public string CurrentFilter { get; set; }
        //public string CurrentCatFilter { get; set; }
        //public string CurrentSubCatFilter { get; set; }
        public string CurrentSort { get; set; }

        public IndexModel(WmIdentityDbContext context, IHtmlHelper htmlHelper)
        {
            _context = context;
            this.htmlHelper = htmlHelper;
        }

        //public IList<Product> Product { get;set; }
        public ProductIndexData Product { get; set; }   
        public int ProductId { get; set; }
        public Category Category { get; set; }


        public async Task OnGetAsync(string sortOrder, string catFilter, int? productId, string currentFilter, string searchString, string subCatFilter, List<string> areChecked)
        {

            List<string> SubCategoriesListChecked = new List<string>();

           
                
                        

            CurrentSort = sortOrder;            
            var cat = CatFilter;

            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            CatSort = sortOrder == "CatSort" ? "CatSort_desc" : "CatSort";

            if(searchString == null)
            {
                searchString = currentFilter;
            }

            //filtro por categoria
            CatFilter = catFilter;
            //filtro por subcategoria
            SubCatFilter = subCatFilter;
            //filtro por nome
            CurrentFilter = searchString;

            

            Product = new ProductIndexData();

            Product.SubCategories
                 = await _context.SubCategories.OrderBy(i=>i.Name).AsNoTracking().ToListAsync();

            Product.Products
                 = await _context.Products
                  .Include(i => i.ProductSubcategories)
                  .ThenInclude(i => i.SubCategory)
                  .AsNoTracking()
                  .ToListAsync();


            if (productId != null)
            {
                ProductId = productId.Value;
            }

            //filtro por nome
            if (!String.IsNullOrEmpty(searchString))
            {
                Product.Products = Product.Products.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()));
                                       
            }
            //filtro por categoria
            if (!String.IsNullOrEmpty(CatFilter))
            {
                if (CatFilter == "Químico" || CatFilter == "Quimico")
                {
                    Product.Products = Product.Products.Where(s => s.Category == Category.Químico);

                }
                else
                {
                    Product.Products = Product.Products.Where(s => s.Category == Category.Agrícola);
                }
            }


            AreChecked = areChecked;
            
            //filtro por subcategoria
            if (AreChecked.Any())
            {

                //parei aqui

                var query = await _context.SubCategories.Where(s => AreChecked.Contains(s.Name)).AsNoTracking().ToListAsync();


                //Product.Products = Product.Products.Where(i => i.ProductSubcategories.Any(x => x.SubCategory.Name.Contains(SubCatFilter)));

                //Product.Products = Product.Products.Where(i => i.ProductSubcategories.Any(x => x.SubCategory.Name.Contains(AreChecked.ToString())));

                // var samurai = _context.Samurais.Include(s => s.SecretIdentity)
                //.FirstOrDefault(s => s.Id == 9); //samurai id
                // samurai.SecretIdentity.RealName = "NovoNome2";
                // _context.SaveChanges();

                var test = AreChecked.ToString();

                foreach (var item in AreChecked)
                {
                    Product.Products = from product in Product.Products
                                       where product.ProductSubcategories.Any(s => s.SubCategory.Name.Contains(item))
                                       select product;
                }


              
                                   

                //i.ProductSubcategories.Any(x => x.SubCategory.Name.Contains(SubCatFilter)));
            }

            //sorting
            switch (sortOrder)
            {
                case "name_desc":
                    Product.Products = Product.Products.OrderByDescending(s=>s.Name);
                    break;
                case "CatSort":
                    Product.Products = Product.Products.OrderBy(s => s.Category);
                    break;
                case "CatSort_desc":
                    Product.Products = Product.Products.OrderByDescending(s => s.Category);
                    break;
                default:
                    Product.Products = Product.Products.OrderBy(s => s.Name);
                    break;
            }

        }
    }
}
