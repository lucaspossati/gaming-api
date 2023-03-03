using API.Domain.VM;
using Application.Models;
using AutoMapper;

namespace api.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<CompanyVM, Company>();

            CreateMap<Company, CompanyVM>()
                .ForMember(u => u.People, opt => opt.MapFrom(sp => sp.People));
        }
    }
}
