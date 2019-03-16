using AutoMapper;
using Domain;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PandaPrime.AM
{
    public class PPermissionProfile2 : Profile
    {
        public PPermissionProfile2()
        {
            CreateMap<PPermission, PPermissionDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.SerialNumber))
                .ForMember(dest => dest.PSN, opt => opt.MapFrom(src => src.ParentSN))
                .ForMember(dest => dest.Lv, opt => opt.MapFrom(src => src.Level))
                .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.FilePath))
                .ReverseMap();
        }
    }
}