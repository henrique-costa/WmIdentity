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
    public class EditProductVM
    {
        //aqui, daqui, parei
        [DisplayName("Título do Produto:")]
        [Required]
        public string Title { get; set; }

        [DisplayName("Descrição:")]
        [Required]
        [MaxLength(5000)]
        public string Description { get; set; }

        public IEnumerable<Category> CategoryList { get; set; }

       
        [Required]
        [DisplayName("Descontinuar")]
        public bool Discontinued { get; set; }


        //[Required]
        //public IEnumerable<string> Files { get; set; }

        [Required]
        [DisplayName("Categoria do Produto:")]
        public int SelectedCategory { get; set; }


    }
}
