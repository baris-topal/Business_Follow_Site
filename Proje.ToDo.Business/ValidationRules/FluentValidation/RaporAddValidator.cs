using FluentValidation;
using Proje.ToDo.DTO.DTOs.RaporDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proje.ToDo.Business.ValidationRules.FluentValidation
{
    public class RaporAddValidator : AbstractValidator<RaporAddDto>
    {
        public RaporAddValidator()
        {
            RuleFor(I => I.Tanim).NotNull().WithMessage("Tanım alanı boş geçilemez!");
            RuleFor(I => I.Detay).NotNull().WithMessage("Detay alanı boş geçilemez!");
        }
    }
}
