using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WmIdentity.Models;
using WmIdentity.Services;
using WmIdentity.ViewModels;

namespace WmIdentity.Pages.ProductsVM
{
    public class ProductSubcategoryPageModel : PageModel
    {
        public List<AssignedSubcategoryData> AssignedSubcategoryDataList;


        public void PopulateProductSubcategoryData(WmIdentityDbContext context,
                                               Product product)
        {           
            var allSubcategories = context.SubCategories;  

            var productSubcat = new HashSet<int>(
                product.ProductSubcategories.Select(c => c.SubCategoryId));
            
            AssignedSubcategoryDataList = new List<AssignedSubcategoryData>();
            
            foreach (var subcategory in allSubcategories)
            {                
                AssignedSubcategoryDataList.Add(new AssignedSubcategoryData
                {

                    SubCategoryId = subcategory.Id,
                    Name = subcategory.Name,
                    Checked = productSubcat.Contains(subcategory.Id)
                });
            }
        }

        public void UpdateProductSubcategory(WmIdentityDbContext context,
            string[] selectedSubcategories, Product productToUpdate)
        {
            if (selectedSubcategories == null)
            {
                productToUpdate.ProductSubcategories = new List<ProductSubcategory>();
                return;
            }
            
            var selectedSubcategoriesHS = new HashSet<string>(selectedSubcategories);
            var productSubcategories = new HashSet<int>
                (productToUpdate.ProductSubcategories.Select(c => c.SubCategory.Id));

            foreach (var subcategory in context.SubCategories)
            {                
                if (selectedSubcategoriesHS.Contains(subcategory.Id.ToString()))
                {
                    if (!productSubcategories.Contains(subcategory.Id))
                    {
                        productToUpdate.ProductSubcategories.Add(
                            new ProductSubcategory
                            {
                                ProductId = productToUpdate.Id,
                                SubCategoryId = subcategory.Id,
                            });
                    }
                }
                else
                {
                    if (productSubcategories.Contains(subcategory.Id))
                    {
                        ProductSubcategory subcategoryToRemove
                            = productToUpdate
                                .ProductSubcategories
                                .SingleOrDefault(i => i.SubCategoryId == subcategory.Id);
                        context.Remove(subcategoryToRemove);
                    }
                }
            }
        }
    }
}
