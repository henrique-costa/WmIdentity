using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WmIdentity.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "EmailAdd")]
        [Required(ErrorMessage = "Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Campo Senha obrigatório!")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password, ErrorMessage = "SenhaInvalida")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Campo Confirmação de Senha obrigatório!")]
        [Display(Name ="Confirmação de Senha")]
        [Compare("Password", ErrorMessage = "Senha e Confirmação de senha não são iguais!")]
        [DataType(DataType.Password)]            
        public string ConfirmPassword { get; set; }

    }
}
