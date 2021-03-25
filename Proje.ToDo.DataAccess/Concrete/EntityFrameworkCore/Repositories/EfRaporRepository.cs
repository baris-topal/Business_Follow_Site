using Microsoft.EntityFrameworkCore;
using Proje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using Proje.ToDo.DataAccess.Interfaces;
using Proje.ToDo.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfRaporRepository : EfGenericRepository<Rapor>, IRaporDal
    {
        public Rapor GetirGorevileId(int id)
        {
            using (var context = new ToDoContext())
            {
                return context.Raporlar.Include(I => I.Gorev).ThenInclude(I=>I.Aciliyet).Where(I => I.Id == id).FirstOrDefault();
            }
        }

        public int GetirRaporSayisi()
        {
            using (var context = new ToDoContext())
            {
                return context.Raporlar.Count();
            }
        }

        public int GetirRaporSayisiileAppUserId(int id)
        {
            using var context = new ToDoContext();
            var result = context.Gorevler.Include(I => I.Raporlar).Where(I => I.AppUserId ==id);
            return result.SelectMany(I => I.Raporlar).Count();
        }
    }
}
