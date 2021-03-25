using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proje.ToDo.Business.Interfaces;
using Proje.ToDo.DTO.DTOs.AciliyetDTOs;
using Proje.ToDo.Entities.Concrete;
using Proje.ToDo.Web.StringInfo;

namespace Proje.ToDo.Web.Areas.Admin.Controllers
{
    [Area(AreaInfo.Admin)]
    [Authorize(Roles = RoleInfo.Admin)]
    public class AciliyetController : Controller
    {
        private readonly IAciliyetService _aciliyetService;
        private readonly IMapper _mapper;
        public AciliyetController(IAciliyetService aciliyetService, IMapper mapper)
        {
            _mapper = mapper;
            _aciliyetService = aciliyetService;
        }
        public IActionResult Index()
        {
            TempData["Active"] = TempdataInfo.Aciliyet;
            #region MyRegion
            //List<Aciliyet> aciliyetler = _aciliyetService.GetirHepsi();

            //List<AciliyetListViewModel> model = new List<AciliyetListViewModel>();

            //foreach (var item in aciliyetler)
            //{
            //    AciliyetListViewModel aciliyetModel = new AciliyetListViewModel();
            //    aciliyetModel.Id = item.Id;
            //    aciliyetModel.Tanim = item.Tanim;

            //    model.Add(aciliyetModel);
            //} 
            #endregion
            return View(_mapper.Map<List<AciliyetListDto>>(_aciliyetService.GetirHepsi()));

        }
        public IActionResult EkleAciliyet()
        {
            TempData["Active"] = TempdataInfo.Aciliyet;
            return View(new AciliyetAddDto());
        }
        [HttpPost]
        public IActionResult EkleAciliyet(AciliyetAddDto model)
        {
            if (ModelState.IsValid)
            {
                _aciliyetService.Kaydet(new Aciliyet()
                {
                    Tanim = model.Tanim
                });
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult GuncelleAciliyet(int id)
        {
            TempData["Active"] = TempdataInfo.Aciliyet;
            #region MyRegion
            //var aciliyet = _aciliyetService.GetirIdile(id);
            //AciliyetUpdateViewModel model = new AciliyetUpdateViewModel
            //{
            //    Id = aciliyet.Id,
            //    Tanim = aciliyet.Tanim
            //}; 
            #endregion
            return View(_mapper.Map<AciliyetUpdateDto>(_aciliyetService.GetirIdile(id)));
        }
        [HttpPost]
        public IActionResult GuncelleAciliyet(AciliyetUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                _aciliyetService.Guncelle(new Aciliyet
                {
                    Id = model.Id,
                    Tanim = model.Tanim
                });
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
