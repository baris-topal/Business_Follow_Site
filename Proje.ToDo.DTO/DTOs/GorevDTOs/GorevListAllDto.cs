using Proje.ToDo.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proje.ToDo.DTO.DTOs.GorevDTOs
{
    public class GorevListAllDto
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        public DateTime OlusturulmaTarihi { get; set; }

        public Aciliyet Aciliyet { get; set; }

        public AppUser AppUser { get; set; }

        public List<Rapor> Raporlar { get; set; }
    }
}
