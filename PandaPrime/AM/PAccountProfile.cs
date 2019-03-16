using AutoMapper;
using Domain;
using Model;

namespace PandaPrime.AM
{
    public class PAccountProfile : Profile
    {
        public PAccountProfile()
        {
            CreateMap<PAccount, PAccountVm>().ReverseMap();
        }
    }
}