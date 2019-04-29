using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WmIdentity.Services;

namespace WmIdentity.Pages.ProductsVM
{
    public class SubcategoryNamePageModel : PageModel
    {
        public SelectList SubcategoryNameSL { get; set; }

        public void PopulateSubcategoryDropDownList(WmIdentityDbContext _context,
            object selectedSubcategory = null)
        {
            var subcategoriesQuery = from d in _context.SubCategories
                                   orderby d.Name // Sort by name.
                                   select d;

            SubcategoryNameSL = new SelectList(subcategoriesQuery.AsNoTracking(),
                        "Id", "Name", selectedSubcategory);
        }
    }
}
