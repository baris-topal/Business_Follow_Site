using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Proje.ToDo.Business.Interfaces;
using Proje.ToDo.DTO.DTOs.GorevDTOs;
using Proje.ToDo.Entities.Concrete;
using Proje.ToDo.Web.BaseControllers;
using Proje.ToDo.Web.StringInfo;

namespace Proje.ToDo.Web.Areas.Member.Controllers
{
    [Area(AreaInfo.Member)]
    [Authorize(Roles = RoleInfo.Member)]
    public class GorevController : BaseIdentityController
    {
        private readonly IGorevService _gorevService;
        private readonly IMapper _mapper;
        public GorevController(IGorevService gorevService, UserManager<AppUser> userManager, IMapper mapper) : base(userManager)
        {
            _mapper = mapper;
            _gorevService = gorevService;
        }
        public async Task<IActionResult> Index(int aktifSayfa = 1)
        {
            TempData["Active"] = TempdataInfo.Gorev;

            var user = await GetirGirisYapanKullanici();

            var gorevler = _mapper.Map<List<GorevListAllDto>>(_gorevService.GetirTumTablolarlaTamamlanmayan(out int toplamSayfa, user.Id, aktifSayfa));

            ViewBag.ToplamSayfa = toplamSayfa;
            ViewBag.AktifSayfa = aktifSayfa;
            #region MyRegion
            //List<GorevListAllViewModel> models = new List<GorevListAllViewModel>();
            //foreach (var gorev in gorevler)
            //{
            //    GorevListAllViewModel model = new GorevListAllViewModel();
            //    model.Id = gorev.Id;
            //    model.Aciklama = gorev.Aciklama;
            //    model.Aciliyet = gorev.Aciliyet;
            //    model.Ad = gorev.Ad;
            //    model.AppUser = gorev.AppUser;
            //    model.OlusturulmaTarihi = gorev.OlusturulmaTarihi;
            //    model.Raporlar = gorev.Raporlar;
            //    models.Add(model);
            //} 
            #endregion
            return View(gorevler);
        }
    }
}
