using FluentValidation;
using Proje.ToDo.DTO.DTOs.AppUserDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proje.ToDo.Business.ValidationRules.FluentValidation
{
    public class AppUserSingInValidator : AbstractValidator<AppUserSignInDto>
    {
        public AppUserSingInValidator()
        {
            RuleFor(I => I.UserName).NotNull().WithMessage("Kullanıcı adı boş geçilemez!");
            RuleFor(I => I.Password).NotNull().WithMessage("Parola alanı boş geçilemez!");
        }
    }
}
