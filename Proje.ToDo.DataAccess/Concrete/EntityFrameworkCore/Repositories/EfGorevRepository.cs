using Microsoft.EntityFrameworkCore;
using Proje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using Proje.ToDo.DataAccess.Interfaces;
using Proje.ToDo.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Proje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfGorevRepository : EfGenericRepository<Gorev>, IGorevDal
    {
        public Gorev GetirAciliyetileId(int id)
        {
            using (var context = new ToDoContext())
            {
                return context.Gorevler.Include(I => I.Aciliyet).FirstOrDefault(I => !I.Durum && I.Id == id);
            };
        }

        public Gorev GetirRaporlarileId(int id)
        {
            using (var context = new ToDoContext())
            {
                return context.Gorevler.Include(I => I.Raporlar).Include(I => I.AppUser).Where(I => I.Id == id).FirstOrDefault();
            };
        }

        public List<Gorev> GetirAciliyetIleTamamlanmayan()
        {
            using (var context = new ToDoContext())
            {
                return context.Gorevler.Include(I => I.Aciliyet).Where(I => !I.Durum).OrderByDescending(I => I.OlusturulmaTarihi).ToList();
            };
        }

        public List<Gorev> GetirileAppUserId(int appUserId)
        {
            using (var context = new ToDoContext())
            {
                return context.Gorevler.Where(I => I.AppUserId == appUserId).ToList();
            };
        }

        public List<Gorev> GetirTumTablolarla()
        {
            using (var context = new ToDoContext())
            {
                return context.Gorevler.Include(I => I.Aciliyet).Include(I => I.Aciliyet).Include(I => I.Raporlar).Include(I => I.AppUser)
                    .Where(I => !I.Durum).OrderByDescending(I => I.OlusturulmaTarihi).ToList();
            };
        }

        public List<Gorev> GetirTumTablolarla(Expression<Func<Gorev, bool>> filter)
        {
            using (var context = new ToDoContext())
            {
                return context.Gorevler.Include(I => I.Aciliyet).Include(I => I.Aciliyet).Include(I => I.Raporlar).Include(I => I.AppUser)
                    .Where(filter).OrderByDescending(I => I.OlusturulmaTarihi).ToList();
            };
        }

        public List<Gorev> GetirTumTablolarlaTamamlanmayan(out int toplamSayfa, int userId, int aktifSayfa = 1)
        {
            using (var context = new ToDoContext())
            {
                var returnValue = context.Gorevler.Include(I => I.Aciliyet).Include(I => I.Aciliyet).Include(I => I.Raporlar).
                    Include(I => I.AppUser).Where(I => I.AppUserId == userId && I.Durum == true).
                    OrderByDescending(I => I.OlusturulmaTarihi);
                toplamSayfa = (int)Math.Ceiling((double)returnValue.Count() / 3);

                return returnValue.Skip((aktifSayfa - 1) * 3).Take(3).ToList();
            };
        }

        public int GetirGorevSayisiTamamlananileAppUserId(int id)
        {
            using var context = new ToDoContext();
            return context.Gorevler.Count(I => I.AppUserId == id && I.Durum);
        }

        public int GetirGorevSayisiTamamlanmasıGerekenAppUserId(int id)
        {
            using var context = new ToDoContext();
            return context.Gorevler.Count(I => I.AppUserId == id && !I.Durum);
        }

        public int GetirAtanmayıBekleyenGorevSayisi()
        {
            using var context = new ToDoContext();
            return context.Gorevler.Count(I => I.AppUserId == null && !I.Durum);
        }

        public int GetirGorevSayisiTamamlanmis()
        {
            using var context = new ToDoContext();
            return context.Gorevler.Count(I => I.Durum);
        }
    }
}
