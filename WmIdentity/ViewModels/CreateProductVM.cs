using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WmIdentity.Models;

namespace WmIdentity.ViewModels
{
    public class CreateProductVM
    {
        public int ProductId { get; set; }

        [DisplayName("Título do Produto:")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Descrição:")]
        [Required]
        [MaxLength(5000)]
        public string Description { get; set; }

        //[Required]
        public string Photo { get; set; }

        [Required]
        [DisplayName("Categoria do Produto:")]
        public Category Category { get; set; }

        public ICollection<ProductSubcategory> SubCategories { get; set; }

    }
}
