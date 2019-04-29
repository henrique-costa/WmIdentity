using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WmIdentity.Models
{
    public class Product
    {

        public int Id { get; set; } 

        public string Name { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

        public string PhotoPath { get; set; }

        public ICollection<ProductSubcategory> ProductSubcategories { get; set; }

    }

}
