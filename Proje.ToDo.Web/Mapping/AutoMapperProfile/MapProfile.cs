using AutoMapper;
using Proje.ToDo.DTO.DTOs.AciliyetDTOs;
using Proje.ToDo.DTO.DTOs.AppUserDTOs;
using Proje.ToDo.DTO.DTOs.BildirimDTOs;
using Proje.ToDo.DTO.DTOs.GorevDTOs;
using Proje.ToDo.DTO.DTOs.RaporDTOs;
using Proje.ToDo.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proje.ToDo.Web.Mapping.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            #region Aciliyet-AciliyetDTO
            CreateMap<AciliyetAddDto, Aciliyet>();
            CreateMap<Aciliyet, AciliyetAddDto>();

            CreateMap<AciliyetListDto, Aciliyet>();
            CreateMap<Aciliyet, AciliyetListDto>();

            CreateMap<AciliyetUpdateDto, Aciliyet>();
            CreateMap<Aciliyet, AciliyetUpdateDto>();
            #endregion

            #region AppUser-AppUserDTO
            CreateMap<AppUserAddDto, AppUser>();
            CreateMap<AppUser, AppUserAddDto>();

            CreateMap<AppUserListDto, AppUser>();
            CreateMap<AppUser, AppUserListDto>();

            CreateMap<AppUserSignInDto, AppUser>();
            CreateMap<AppUser, AppUserSignInDto>();
            #endregion

            #region Bildirim-BildirimDTO
            CreateMap<BildirimListDto, Bildirim>();
            CreateMap<Bildirim, BildirimListDto>();
            #endregion

            #region Gorev-GorevDTO
            CreateMap<GorevAddDto, Gorev>();
            CreateMap<Gorev, GorevAddDto>();

            CreateMap<GorevListDto, Gorev>();
            CreateMap<Gorev, GorevListDto>();

            CreateMap<GorevUpdateDto, Gorev>();
            CreateMap<Gorev, GorevUpdateDto>();

            CreateMap<GorevListAllDto, Gorev>();
            CreateMap<Gorev, GorevListAllDto>();
            #endregion

            #region Rapor-RaporDTO
            CreateMap<RaporAddDto, Rapor>();
            CreateMap<Rapor, RaporAddDto>();

            CreateMap<Rapor, RaporUpdateDto>();
            CreateMap<RaporUpdateDto, Rapor>();

            CreateMap<Rapor, RaporDosyaDto>();
            CreateMap<RaporDosyaDto, Rapor>();
            #endregion
        }
    }
}
