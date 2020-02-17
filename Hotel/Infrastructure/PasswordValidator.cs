using Hotel.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Infrastructure
{
    /// <summary>
    /// Кастомный валидатор паролей, унаследован от PasswordValidator<AppUser>
    /// </summary>
    public class PasswordValidator : PasswordValidator<AppUser>
    {
        public override async Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            IdentityResult result = await base.ValidateAsync(manager, user, password);

            List<IdentityError> errors = result.Succeeded ?
                new List<IdentityError>() : result.Errors.ToList();

            if (password.ToLower().Contains(user.UserName.ToLower()))
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordContainsUserName",
                    Description = "Пароль не может содержать имя пользователя"
                });
            }
            if (password.Contains("12345"))
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordContainsSequence",
                    Description = "Пароль не может содержать числовую последовательность"
                });
            }

            return errors.Count == 0 ?
                IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
        }
    }
}
