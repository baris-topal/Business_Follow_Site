using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Proje.ToDo.Business.Interfaces;
using Proje.ToDo.DTO.DTOs.AppUserDTOs;
using Proje.ToDo.DTO.DTOs.GorevDTOs;
using Proje.ToDo.DTO.DTOs.RaporDTOs;
using Proje.ToDo.Entities.Concrete;
using Proje.ToDo.Web.StringInfo;

namespace Proje.ToDo.Web.Areas.Admin.Controllers
{
    [Area(AreaInfo.Admin)]
    [Authorize(Roles = RoleInfo.Admin)]
    public class IsEmriController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IGorevService _gorevService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IDosyaService _dosyaService;
        private readonly IBildirimService _bildirimService;
        private readonly IMapper _mapper;
        public IsEmriController(IAppUserService appUserService, IGorevService gorevService, UserManager<AppUser> userManager,
            IDosyaService dosyaService, IBildirimService bildirimService, IMapper mapper)
        {
            _mapper = mapper;
            _bildirimService = bildirimService;
            _userManager = userManager;
            _gorevService = gorevService;
            _appUserService = appUserService;
            _dosyaService = dosyaService;
        }
        public IActionResult Index()
        {
            TempData["Active"] = TempdataInfo.IsEmri;
            #region MyRegion
            //var model = _appUserService.GetirAdminOlmayanlar();
            //List<Gorev> gorevler = _gorevService.GetirTumTablolarla();
            //List<GorevListAllViewModel> models = new List<GorevListAllViewModel>();

            //foreach (var item in gorevler)
            //{
            //    GorevListAllViewModel model = new GorevListAllViewModel();
            //    model.Id = item.Id;
            //    model.Aciklama = item.Aciklama;
            //    model.Ad = item.Ad;
            //    model.Aciliyet = item.Aciliyet;
            //    model.AppUser = item.AppUser;
            //    model.OlusturulmaTarihi = item.OlusturulmaTarihi;
            //    model.Raporlar = item.Raporlar;
            //    models.Add(model);
            //} 
            #endregion
            return View(_mapper.Map<List<GorevListAllDto>>(_gorevService.GetirTumTablolarla()));

        }
        public IActionResult AtaPersonel(int id, string s, int sayfa = 1)
        {
            TempData["Active"] = TempdataInfo.IsEmri;
            ViewBag.AktifSayfa = sayfa;
            #region MyRegion
            //ViewBag.ToplamSayfa = (int)Math.Ceiling((double)_appUserService.GetirAdminOlmayanlar().Count / 3);
            //var personeller = _appUserService.GetirAdminOlmayanlar(out toplamSayfa, s, sayfa); 
            #endregion
            ViewBag.Aranan = s;
            var personeller = _mapper.Map<List<AppUserListDto>>(_appUserService.GetirAdminOlmayanlar(out int toplamSayfa, s, sayfa));
            ViewBag.toplamSayfa = toplamSayfa;
            #region MyRegion
            //List<AppUserListViewModel> appUserListModel = new List<AppUserListViewModel>();
            //foreach (var item in personeller)
            //{
            //    AppUserListViewModel model = new AppUserListViewModel();
            //    model.Id = item.Id;
            //    model.Name = item.Name;
            //    model.SurName = item.Surname;
            //    model.Email = item.Email;
            //    model.Picture = item.Picture;
            //    appUserListModel.Add(model);
            //} 
            #endregion
            ViewBag.Personeller = personeller;
            #region MyRegion
            //var gorev = _gorevService.GetirAciliyetileId(id);

            //GorevListViewModel gorevModel = new GorevListViewModel();
            //gorevModel.Id = gorev.Id;
            //gorevModel.Ad = gorev.Ad;
            //gorevModel.Aciklama = gorev.Aciklama;
            //gorevModel.Aciliyet = gorev.Aciliyet;
            //gorevModel.OlusturulmaTarihi = gorev.OlusturulmaTarihi; 
            #endregion
            return View(_mapper.Map<GorevListDto>(_gorevService.GetirAciliyetileId(id)));
        }
        [HttpPost]
        public IActionResult AtaPersonel(PersonelGorevlendirDto model)
        {
            var guncellenecekGorev = _gorevService.GetirIdile(model.GorevId);
            guncellenecekGorev.AppUserId = model.PersonelId;
            _gorevService.Guncelle(guncellenecekGorev);

            _bildirimService.Kaydet(new Bildirim
            {
                AppUserId = model.PersonelId,
                Aciklama = $"{guncellenecekGorev.Ad} adlı iş için görevlendirildiniz.",
            });

            return RedirectToAction("Index");
        }
        public IActionResult GorevlendirPersonel(PersonelGorevlendirDto model)
        {
            TempData["Active"] = TempdataInfo.IsEmri;
            PersonelGorevlendirListDto personelGorevlendirModel = new PersonelGorevlendirListDto
            {
                AppUser = _mapper.Map<AppUserListDto>(_userManager.Users.FirstOrDefault(I => I.Id == model.PersonelId))
            };
            personelGorevlendirModel.Gorev = _mapper.Map<GorevListDto>(_gorevService.GetirAciliyetileId(model.GorevId));
            #region MyRegion
            //var user = _userManager.Users.FirstOrDefault(I => I.Id == model.PersonelId);
            //AppUserListViewModel userModel = new AppUserListViewModel();
            //userModel.Id = user.Id;
            //userModel.Name = user.Name;
            //userModel.SurName = user.Surname;
            //userModel.Picture = user.Picture;
            //userModel.Email = user.Email; 
            #endregion
            #region MyRegion
            //var gorev = _gorevService.GetirAciliyetileId(model.GorevId);
            //GorevListViewModel gorevModel = new GorevListViewModel();
            //gorevModel.Id = gorev.Id;
            //gorevModel.Aciklama = gorev.Aciklama;
            //gorevModel.Aciliyet = gorev.Aciliyet;
            //gorevModel.Ad = gorev.Ad; 
            #endregion
            return View(personelGorevlendirModel);
        }
        public IActionResult Detaylandir(int id)
        {
            TempData["Active"] = TempdataInfo.IsEmri;
            #region MyRegion
            //var gorev = _gorevService.GetirRaporlarileId(id);
            //GorevListAllViewModel model = new GorevListAllViewModel();
            //model.Id = gorev.Id;
            //model.Raporlar = gorev.Raporlar;
            //model.Ad = gorev.Ad;
            //model.Aciklama = gorev.Aciklama;
            //model.AppUser = gorev.AppUser; 
            #endregion
            return View(_mapper.Map<GorevListAllDto>(_gorevService.GetirRaporlarileId(id)));

        }
        public IActionResult GetirExcel(int id)
        {
            return File(_dosyaService.AktarExcel(_mapper.Map<List<RaporDosyaDto>>(_gorevService.GetirRaporlarileId(id).Raporlar)),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Guid.NewGuid() + ".xlsx");
        }
        public IActionResult GetirPdf(int id)
        {
            var path = _dosyaService.AktarPdf(_mapper.Map<List<RaporDosyaDto>>(_gorevService.GetirRaporlarileId(id).Raporlar));
            return File(path, "application/pdf", Guid.NewGuid() + ".pdf");
        }
    }
}
