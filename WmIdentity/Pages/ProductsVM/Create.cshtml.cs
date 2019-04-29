using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WmIdentity.Models;
using WmIdentity.Services;

namespace WmIdentity.Pages.ProductsVM
{
    public class CreateModel : PageModel
    {
        private readonly WmIdentityDbContext _context;
        private readonly IHtmlHelper htmlHelper;

        public CreateModel(WmIdentityDbContext context, IHtmlHelper htmlHelper)
        {
            _context = context;
            this.htmlHelper = htmlHelper;
        }


        [BindProperty]
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

        public IActionResult OnGet()
        {
            Categories = htmlHelper.GetEnumSelectList<Category>();

            return Page();
        }

        

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}