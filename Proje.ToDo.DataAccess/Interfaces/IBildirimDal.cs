using Proje.ToDo.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proje.ToDo.DataAccess.Interfaces
{
    public interface IBildirimDal : IGenericDal<Bildirim>
    {
        List<Bildirim> GetirOkunmayanlar(int AppUserId);
        int GetirOkunmayanSayisiileAppUserId(int AppUserId);

    }
}
