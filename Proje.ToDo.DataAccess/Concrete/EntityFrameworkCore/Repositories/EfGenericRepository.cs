using Proje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using Proje.ToDo.DataAccess.Interfaces;
using Proje.ToDo.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfGenericRepository<Tablo> : IGenericDal<Tablo> where Tablo : class, ITablo, new()
    {
        public List<Tablo> GetirHepsi()
        {
            using (var context = new ToDoContext())
            {
                return context.Set<Tablo>().ToList();
            }
        }

        public Tablo GetirIdile(int id)
        {
            using (var context = new ToDoContext())
            {
                return context.Set<Tablo>().Find(id);
            }
        }

        public void Guncelle(Tablo tablo)
        {
            using (var context = new ToDoContext())
            {
                context.Set<Tablo>().Update(tablo);
                context.SaveChanges();
            }
        }

        public void Kaydet(Tablo tablo)
        {
            using (var context = new ToDoContext())
            {
                context.Set<Tablo>().Add(tablo);
                context.SaveChanges();
            }
        }

        public void Sil(Tablo tablo)
        {
            using (var context = new ToDoContext())
            {
                context.Set<Tablo>().Remove(tablo);
                context.SaveChanges();
            }
        }
    }
}
