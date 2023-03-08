using Application.Models;
using AutoMapper;
using Manager.VM.Company;

namespace api.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<CompanyVM, Company>();

            CreateMap<NewCompanyVM, CompanyVM>();

            CreateMap<NewCompanyVM, Company>();

            CreateMap<Company, CompanyVM>()
                .ForMember(u => u.Persons, opt => opt.MapFrom(sp => sp.People));
        }
    }
}
