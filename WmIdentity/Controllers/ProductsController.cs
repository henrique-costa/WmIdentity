using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WmIdentity.Models;
using WmIdentity.Services;
using X.PagedList;
using WmIdentity.ViewModels;
using Microsoft.EntityFrameworkCore.Storage;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace WmIdentity.Controllers
{
    public class ProductsController : Controller
    {
        private readonly WmIdentityDbContext _context;
        public List<string> AreChecked = new List<string>();
        private readonly IHtmlHelper htmlHelper;
        private IHostingEnvironment _environment;


        public IEnumerable<SelectListItem> Categories { get; set; }



        public ProductsController(WmIdentityDbContext context, IHtmlHelper htmlHelper, IHostingEnvironment environment)
        {
            _context = context;
            this.htmlHelper = htmlHelper;
            _environment = environment;
        }

        // GET: Products
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, List<string> areChecked, string catSelected)
        {

            List<string> SubCategoriesListChecked = new List<string>();
            AreChecked = areChecked;

            ViewBag.Subcategories = from item in _context.SubCategories
                                    orderby item.Name
                                    select item.Name
                                    ;


            ViewBag.CurrentSort = sortOrder;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";


          

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            


            var viewModel = new ProductIndexData();
            viewModel.Products = _context.Products
                .Include(i => i.ProductSubcategories)
                .ThenInclude(i => i.SubCategory).AsNoTracking();


            if (!String.IsNullOrEmpty(searchString))
            {
                viewModel.Products = viewModel.Products.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()));
            }

            //filtro por categoria
            if (!String.IsNullOrEmpty(catSelected))
            {
                if (catSelected == "Químico" || catSelected == "Quimico")
                {
                    viewModel.Products = viewModel.Products.Where(s => s.Category == Category.Químico);

                }
                else
                {
                    viewModel.Products = viewModel.Products.Where(s => s.Category == Category.Agrícola);
                }
            }

            //filtro por subcategoria
            if (AreChecked.Any())
            {

                //var test = AreChecked.ToString();

                foreach (var item in AreChecked)
                {
                    viewModel.Products = from product in viewModel.Products
                                         where product.ProductSubcategories.Any(s => s.SubCategory.Name.Contains(item))
                                       select product;
                }
            }

            switch (sortOrder)
            {
                case "name_desc":
                    viewModel.Products = viewModel.Products.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    viewModel.Products = viewModel.Products.OrderBy(s => s.Category);
                    break;
                case "date_desc":
                    viewModel.Products = viewModel.Products.OrderByDescending(s => s.Category);
                    break;
                default:
                    viewModel.Products = viewModel.Products.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(viewModel.Products.ToPagedList(pageNumber, pageSize));
        }




        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id, DetailsVM vm)
        {
            


            if (id == null)
            {
                return NotFound();
            }

            vm.Product = await _context.Products
                .Include(m => m.ProductSubcategories).ThenInclude(m => m.SubCategory)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);

            if (vm.Product == null)
            {
                return NotFound();
            }

            vm.RelatedProducts = await _context.Products
                .Include(m=>m.ProductSubcategories)
                .ThenInclude(m => m.SubCategory)
                .Where(m => m.Category == vm.Product.Category)
                .ToListAsync();

            //var query =
            //from post in context.Posts
            //where tagIds.All(requiredId => post.Tags.Any(tag => tag.TagId == requiredId))
            //select post;

            var subCatList = from item in vm.Product.ProductSubcategories
                             select item.SubCategoryId;


            vm.RelatedProducts = from item in vm.RelatedProducts
                                 where subCatList.Any(requieredId => item.ProductSubcategories.Any(subCatId => subCatId.SubCategoryId == requieredId))
                                 select item;

            if (vm.RelatedProducts != null)
            {
                vm.RelatedProducts = vm.RelatedProducts.Where(m => m.Id != vm.Product.Id);
            }

            //foreach (var item in subCatList)
            //{
            //    vm.RelatedProducts = from prod in vm.RelatedProducts
            //                         where 
            //}

                       

            return View(vm);
        }
        

        // GET: Products/Create
        public IActionResult Create()
        {
            var product = new Product();
            ViewBag.Categories = new SelectList(htmlHelper.GetEnumSelectList<Category>());

            product.ProductSubcategories= new List<ProductSubcategory>();
            PopulateAssignedSubCategoryData(product);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateProductVM vm, IFormFile photoPath, string[] selectedSubcat)
        {
            try
            {
                ViewBag.Categories = htmlHelper.GetEnumSelectList<Category>();

                if (selectedSubcat != null)
                {
                    vm.SubCategories = new List<ProductSubcategory>();

                    foreach (var subCat in selectedSubcat)
                    {
                        var subCatToAdd = new ProductSubcategory { ProductId = vm.ProductId, SubCategoryId = int.Parse(subCat) };
                        vm.SubCategories.Add(subCatToAdd);
                    }
                }               

                if (ModelState.IsValid)
                {
                    Product product = new Product
                    {
                        Name = vm.Name,
                        Description = vm.Description,
                        Category = vm.Category,
                        ProductSubcategories = vm.SubCategories
                        
                    };

                    if (photoPath != null)
                    {
                        //1 -) create directory
                        string uploadPath = Path.Combine(_environment.WebRootPath, "Uploads\\Products");
                        //uploadPath = Path.Combine(uploadPath, product.Name);
                        Directory.CreateDirectory(Path.Combine(uploadPath, product.Name));
                        //2 -) get the file name
                        string FileName = Path.GetFileName(photoPath.FileName);

                        //3 -) 
                        using (FileStream fs = new FileStream(Path.Combine(uploadPath, product.Name, FileName), FileMode.Create))
                        {
                            photoPath.CopyTo(fs);
                        }
                        //4 -) change the pack photoPath
                        product.PhotoPath = Path.Combine(product.Name, FileName);
                    }
                    else
                    {
                        product.PhotoPath = "";
                    }


                    _context.Products.Add(product);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            
            return View(vm);
        }

        

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(i => i.ProductSubcategories)
                .ThenInclude(i => i.SubCategory)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            PopulateAssignedSubCategoryData(product);
            return View(product);
        }

        private void PopulateAssignedSubCategoryData(Product product)
        {
            var allSubCats = _context.SubCategories;
            var ProductsSubCategory = new HashSet<int>(product.ProductSubcategories.Select(c => c.SubCategoryId));
            var viewModel = new List<AssignedSubcategoryData>();
            foreach (var item in allSubCats)
            {
                viewModel.Add(new AssignedSubcategoryData
                {
                    SubCatId = item.Id,
                    Title = item.Name,
                    Assigned = ProductsSubCategory.Contains(item.Id)
                });
            }
            ViewData["SubCategories"] = viewModel;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedSubCategories, IFormFile photoPath)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productToUpdate = await _context.Products
                .Include(i => i.ProductSubcategories)
                .ThenInclude(i => i.SubCategory)

                .SingleOrDefaultAsync(m => m.Id == id);

            if (photoPath != null)
            {
                //1 -) create directory
                string uploadPath = Path.Combine(_environment.WebRootPath, "Uploads\\Products");
                //uploadPath = Path.Combine(uploadPath, product.Name);
                Directory.CreateDirectory(Path.Combine(uploadPath, productToUpdate.Name));
                //2 -) get the file name
                string FileName = Path.GetFileName(photoPath.FileName);

                //3 -) 
                using (FileStream fs = new FileStream(Path.Combine(uploadPath, productToUpdate.Name, FileName), FileMode.Create))
                {
                    photoPath.CopyTo(fs);
                }
                //4 -) change the pack photoPath
                productToUpdate.PhotoPath = Path.Combine(productToUpdate.Name, FileName);
            }
            else
            {
                productToUpdate.PhotoPath = productToUpdate.PhotoPath;
            }            

            if (await TryUpdateModelAsync<Product>(
                productToUpdate,
                "",
                i => i.Name, i=>i.Description, i=>i.Category))
            {

                
                UpdateProductsSubCategories(selectedSubCategories, productToUpdate);


                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateProductsSubCategories(selectedSubCategories, productToUpdate);
            PopulateAssignedSubCategoryData(productToUpdate);
            return View(productToUpdate);
        }

        private void UpdateProductsSubCategories(string[] selectedSubCategories, Product productToUpdate)
        {
            if (selectedSubCategories == null)
            {
                productToUpdate.ProductSubcategories = new List<ProductSubcategory>();
                return;
            }

            var selectedSubCategoriesHS = new HashSet<string>(selectedSubCategories);

            var productSubCategories = new HashSet<int>
                (productToUpdate.ProductSubcategories.Select(c => c.SubCategory.Id));


            foreach (var subcat in _context.SubCategories)
            {
                if (selectedSubCategoriesHS.Contains(subcat.Id.ToString()))
                {
                    if (!productSubCategories.Contains(subcat.Id))
                    {
                        productToUpdate.ProductSubcategories.Add(new ProductSubcategory { ProductId = productToUpdate.Id, SubCategoryId = subcat.Id });
                    }
                }
                else
                {

                    if (productSubCategories.Contains(subcat.Id))
                    {
                        ProductSubcategory courseToRemove = productToUpdate.ProductSubcategories.SingleOrDefault(i => i.SubCategoryId == subcat.Id);
                        _context.Remove(courseToRemove);
                    }
                }
            }
        }

        

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

          var product = await _context.Products
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products
                .Include(m=>m.ProductSubcategories)
                .SingleAsync(i=>i.Id == id);


            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        // GET: Products/Delete/5
        public IActionResult Contact()
        {
          

            return View();
        }
    }
}
