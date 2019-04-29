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

namespace WmIdentity.Pages.ProductsVM
{
    public class EditModel : ProductSubcategoryPageModel
    {
        private readonly WmIdentityDbContext _context;
        private readonly IHtmlHelper htmlHelper;

        public EditModel(WmIdentityDbContext context, IHtmlHelper htmlHelper)
        {
            _context = context;
            this.htmlHelper = htmlHelper;
        }

        [BindProperty]
        public Product Product { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public async Task<IActionResult> OnGetAsync(int? productId)
        {

            if (productId == null)
            {
                return RedirectToPage("./NotFound");
            }

            Categories = htmlHelper.GetEnumSelectList<Category>();
            //Product = await _context.Products.FirstOrDefaultAsync(m => m.Id == productId);

            Product = await _context.Products
                .Include(i=>i.ProductSubcategories)
                .ThenInclude(i=>i.SubCategory)
           .AsNoTracking()
           .FirstOrDefaultAsync(m => m.Id == productId);

            if (Product == null)
            {
                return RedirectToPage("./NotFound");
            }
            PopulateProductSubcategoryData(_context, Product);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? productId, string[] selectedSubcategories)
        {
            
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./NotFound");
            }

            _context.Attach(Product).State = EntityState.Modified;

            var productToUpdate = await _context.Products
           
           .Include(i => i.ProductSubcategories)
               .ThenInclude(i => i.SubCategory)
           .FirstOrDefaultAsync(s => s.Id == productId);



            if (await TryUpdateModelAsync<Product>(
                productToUpdate,
                "Product",
                i => i.Name, i => i.Description,
                i => i.Category))
            {
                UpdateProductSubcategory(_context, selectedSubcategories, productToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            UpdateProductSubcategory(_context, selectedSubcategories, productToUpdate);
            PopulateProductSubcategoryData(_context, productToUpdate);
            return Page();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
