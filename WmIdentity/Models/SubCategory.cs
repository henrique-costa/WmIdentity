using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WmIdentity.Models
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //[NotMapped]
        //public bool IsChecked { get; set; }

        public ICollection<ProductSubcategory> ProductSubcategories { get; set; }
    }
}


//Aminoácidos
//Biofertilizantes
//Emulsificantes
//Adjuvantes
//Bioativadores
//Desinfetantes
//Preservantes
//Terpenos
