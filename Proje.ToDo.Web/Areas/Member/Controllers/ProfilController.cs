using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Proje.ToDo.DTO.DTOs.AppUserDTOs;
using Proje.ToDo.Entities.Concrete;
using Proje.ToDo.Web.BaseControllers;
using Proje.ToDo.Web.StringInfo;

namespace Proje.ToDo.Web.Areas.Member.Controllers
{
    [Area(AreaInfo.Member)]
    [Authorize(Roles = RoleInfo.Member)]
    public class ProfilController : BaseIdentityController
    {
        private readonly IMapper _mapper;
        public ProfilController(UserManager<AppUser> userManager, IMapper mapper) : base(userManager)
        {
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = TempdataInfo.Profil;
            var appUser = await GetirGirisYapanKullanici();
            #region MyRegion
            //AppUserListViewModel model = new AppUserListViewModel();
            //model.Id = appUser.Id;
            //model.Name = appUser.Name;
            //model.SurName = appUser.Surname;
            //model.Email = appUser.Email;
            //model.Picture = appUser.Picture; 
            #endregion
            return View(_mapper.Map<AppUserListDto>(appUser));
        }
        [HttpPost]
        public async Task<IActionResult> Index(AppUserListDto model, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                var guncellenecekKullanici = _userManager.Users.FirstOrDefault(I => I.Id == model.Id);
                if (foto != null)
                {
                    string uzanti = Path.GetExtension(foto.FileName);
                    string fotoAd = Guid.NewGuid() + uzanti;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + fotoAd);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await foto.CopyToAsync(stream);
                    }
                    guncellenecekKullanici.Picture = fotoAd;
                }
                guncellenecekKullanici.Name = model.Name;
                guncellenecekKullanici.Surname = model.SurName;
                guncellenecekKullanici.Email = model.Email;

                var result = await _userManager.UpdateAsync(guncellenecekKullanici);
                if (result.Succeeded)
                {
                    TempData["mesaj"] = "Güncelleme işleminiz başarı ile gerçekleşti";
                    return RedirectToAction("Index");
                }
                HataEkle(result.Errors);
            }
            return View();
        }
    }
}
