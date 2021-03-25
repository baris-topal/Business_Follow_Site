using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Proje.ToDo.Business.Interfaces;
using Proje.ToDo.DTO.DTOs.GorevDTOs;
using Proje.ToDo.DTO.DTOs.RaporDTOs;
using Proje.ToDo.Entities.Concrete;
using Proje.ToDo.Web.BaseControllers;
using Proje.ToDo.Web.StringInfo;

namespace Proje.ToDo.Web.Areas.Member.Controllers
{
    [Area(AreaInfo.Member)]
    [Authorize(Roles = RoleInfo.Member)]
    public class IsEmriController : BaseIdentityController
    {
        private readonly IGorevService _gorevService;
        private readonly IRaporService _raporService;
        private readonly IBildirimService _bildirimService;
        private readonly IMapper _mapper;
        public IsEmriController(IGorevService gorevService, IRaporService raporService, UserManager<AppUser> userManager,
            IBildirimService bildirimService, IMapper mapper):base(userManager)
        {
            _mapper = mapper;
            _bildirimService = bildirimService;
            _raporService = raporService;
            _gorevService = gorevService;
        }
        public async Task<IActionResult> Index()
        {
            var user = await GetirGirisYapanKullanici();
            TempData["Active"] = TempdataInfo.IsEmri;
            #region MyRegion
            //List<GorevListAllViewModel> models = new List<GorevListAllViewModel>();
            //foreach (var item in gorevler)
            //{
            //    GorevListAllViewModel model = new GorevListAllViewModel();
            //    model.Id = item.Id;
            //    model.Aciklama = item.Aciklama;
            //    model.Aciliyet = item.Aciliyet;
            //    model.Ad = item.Ad;
            //    model.AppUser = item.AppUser;
            //    model.Raporlar = item.Raporlar;
            //    model.OlusturulmaTarihi = item.OlusturulmaTarihi;
            //    models.Add(model);
            //} 
            #endregion
            return View(_mapper.Map<List<GorevListAllDto>>(_gorevService.GetirTumTablolarla(I => I.AppUserId == user.Id && !I.Durum)));
        }
        public async Task<IActionResult> TamamlaGorev(int gorevId)
        {
            var guncellenecekGorev = _gorevService.GetirIdile(gorevId);
            guncellenecekGorev.Durum = true;
            _gorevService.Guncelle(guncellenecekGorev);

            var adminUserList = await _userManager.GetUsersInRoleAsync("Admin");
            var aktifKullanici = await GetirGirisYapanKullanici();
            foreach (var admin in adminUserList)
            {
                _bildirimService.Kaydet(new Bildirim
                {
                    Aciklama = $"{aktifKullanici.Name} {aktifKullanici.Surname} adlı vermiş olduğunuz personel görevi tamamladı.",
                    AppUserId = admin.Id,
                });
            }
            return Json(null);
        }
        public IActionResult EkleRapor(int id)
        {
            TempData["Active"] = TempdataInfo.IsEmri;
            var gorev = _gorevService.GetirAciliyetileId(id);

            RaporAddDto model = new RaporAddDto
            {
                GorevId = id,
                Gorev = gorev
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EkleRapor(RaporAddDto model)
        {
            if (ModelState.IsValid)
            {
                _raporService.Kaydet(new Rapor()
                {
                    GorevId = model.GorevId,
                    Detay = model.Detay,
                    Tanim = model.Tanim
                });

                var adminUserList = await _userManager.GetUsersInRoleAsync("Admin");
                var aktifKullanici = await GetirGirisYapanKullanici();
                foreach (var admin in adminUserList)
                {
                    _bildirimService.Kaydet(new Bildirim
                    {
                        Aciklama = $"{aktifKullanici.Name} {aktifKullanici.Surname} adlı personel yeni bir rapor yazdı.",
                        AppUserId = admin.Id,
                    });
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult GuncelleRapor(int id)
        {
            TempData["Active"] = TempdataInfo.IsEmri;
            var rapor = _raporService.GetirGorevileId(id);
            RaporUpdateDto model = new RaporUpdateDto
            {
                Id = rapor.Id,
                Tanim = rapor.Tanim,
                Gorev = rapor.Gorev,
                Detay = rapor.Detay,
                GorevId = rapor.GorevId
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult GuncelleRapor(RaporUpdateDto model)
        {
            TempData["Active"] = TempdataInfo.IsEmri;
            if (ModelState.IsValid)
            {
                var guncellenecekRapor = _raporService.GetirIdile(model.Id);
                guncellenecekRapor.Tanim = model.Tanim;
                guncellenecekRapor.Detay = model.Detay;

                _raporService.Guncelle(guncellenecekRapor);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        
    }
}
