using AutoMapper;
using Domain;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PandaPrime.AM
{
    public class PPermissionProfile : Profile
    {
        public PPermissionProfile()
        {
            CreateMap<PPermission, PPermissionVm>().ReverseMap();
        }
    }
}