using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WmIdentity.Models;
using WmIdentity.Services;

namespace WmIdentity.Pages.ProductsVM
{
    public class DeleteModel : PageModel
    {
        private readonly WmIdentityDbContext _context;

        public DeleteModel(WmIdentityDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? productId)
        {
            if (productId == null)
            {
                return RedirectToPage("./NotFound");
            }

            Product = await _context.Products.FirstOrDefaultAsync(m => m.Id == productId);

            if (Product == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? productId)
        {
            if (productId == null)
            {
                return RedirectToPage("./NotFound");
            }

            Product product = await _context.Products
                .Include(i => i.ProductSubcategories)
                .SingleAsync(i => i.Id == productId);

            //Product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            else
            {
                return RedirectToPage("./NotFound");
            }

            return RedirectToPage("./Index");
        }
    }
}
