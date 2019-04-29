using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WmIdentity.ViewModels
{
    public class CustomerMailFormVM
    {
        //string email, string subject, string message

        [Required]
        public string email { get; set; }
        [Required]
        public string nome { get; set; }
        [Required]
        public string subject { get; set; }
        [Required]
        public string message { get; set; }

    }
}
