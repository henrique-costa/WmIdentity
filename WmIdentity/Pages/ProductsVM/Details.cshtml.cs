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
    public class DetailsModel : PageModel
    {
        private readonly WmIdentityDbContext _context;

        public DetailsModel(WmIdentityDbContext context)
        {
            _context = context;
        }

        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            Product = await _context.Products.FirstOrDefaultAsync(m => m.Id == productId);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
