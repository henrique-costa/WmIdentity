using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WmIdentity.Models
{
    public class DoesNotContainPasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : class
    {
        public async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            var username = await manager.GetUserNameAsync(user);

            if (username == password)
                return IdentityResult.Failed(new IdentityError{Description = "Senha não pode conter o Nome de Usuario ou Email."});
            if (password.Contains("password"))
                return IdentityResult.Failed(new IdentityError{Description = "A senha não pode conter a palavra 'password'"});
            if (password.Contains("senha"))
                return IdentityResult.Failed(new IdentityError{Description = "A senha não pode conter a palavra 'senha'" });

            return IdentityResult.Success;
        }
    }
}