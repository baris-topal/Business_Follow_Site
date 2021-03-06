using Proje.ToDo.Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace Proje.ToDo.Entities.Concrete
{
    public class Gorev : ITablo
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        public bool Durum { get; set; }
        public DateTime OlusturulmaTarihi { get; set; } = DateTime.Now;

        public int AciliyetId{ get; set; }
        public Aciliyet Aciliyet{ get; set; }

        public int? AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public List<Rapor> Raporlar { get; set; }
  
    }
}
