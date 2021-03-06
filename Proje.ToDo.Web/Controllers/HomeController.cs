using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Proje.ToDo.Business.Interfaces;
using Proje.ToDo.DTO.DTOs.AppUserDTOs;
using Proje.ToDo.Entities.Concrete;
using Proje.ToDo.Web.BaseControllers;

namespace Proje.ToDo.Web.Controllers
{
    public class HomeController : BaseIdentityController
    {
        private readonly SignInManager<AppUser> _singInManager;
        private readonly ICustomLogger _customLogger;
        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> singInManager, ICustomLogger customLogger) : base(userManager)
        {
            _customLogger = customLogger;
            _singInManager = singInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GirisYap(AppUserSignInDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var identityResult = await _singInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                    if (identityResult.Succeeded)
                    {
                        var roller = await _userManager.GetRolesAsync(user);
                        if (roller.Contains("Admin"))
                        {
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home", new { area = "Member" });
                        }
                    }
                }
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
            }
            return View("Index", model);
        }
        public IActionResult KayitOl()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> KayitOl(AppUserAddDto model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.Surname,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var addRoleResult = await _userManager.AddToRoleAsync(user, "Member");
                    if (addRoleResult.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    HataEkle(addRoleResult.Errors);
                }
                HataEkle(result.Errors);
            }
            return View(model);
        }
        public async Task<IActionResult> CikisYap()
        {
            await _singInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
        public IActionResult StatusCode(int? code)
        {
            if (code == 404)
            {
                ViewBag.Code = code;
                ViewBag.Message = "Sayfa bulunamadı!";
            }
            return View();
        }
        public IActionResult Error()
        {
            var exceptionHandler = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _customLogger.LogError($"Hatanın oluştuğu yer: { exceptionHandler.Path}\nHatanın mesajı: {exceptionHandler.Error.Message}" +
                $"\nStack Trace: {exceptionHandler.Error.StackTrace}");
            ViewBag.Path=exceptionHandler.Path;
            ViewBag.Message = exceptionHandler.Error.Message;
            return View();
        }
        public void Hata()
        {
            throw new Exception("bu bir hata");
        }
    }
}
