using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WmIdentity.Models;

namespace WmIdentity.ViewModels
{
    public class DisplaySingleProductVM
    {
        //public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> PhotoPaths { get; set; }
        public int CategoryId { get; set; }
        //public bool Discontinued { get; set; }

    }
}
