using Proje.ToDo.DTO.DTOs.GorevDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proje.ToDo.DTO.DTOs.AppUserDTOs
{
    public class PersonelGorevlendirListDto
    {
        public AppUserListDto AppUser { get; set; }
        public GorevListDto Gorev { get; set; }
    }
}
