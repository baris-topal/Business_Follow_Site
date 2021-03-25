using FluentValidation;
using Proje.ToDo.DTO.DTOs.GorevDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proje.ToDo.Business.ValidationRules.FluentValidation
{
    public class GorevAddValidator : AbstractValidator<GorevAddDto>
    {
        public GorevAddValidator()
        {
            RuleFor(I => I.Ad).NotNull().WithMessage("Ad alanı boş geçilemez!");
            RuleFor(I => I.AciliyetId).ExclusiveBetween(0,int.MaxValue).WithMessage("Aciliyet durumu seçiniz!");
        }
    }
}
