
using API.Domain.VM;
using Application.Models;
using AutoMapper;


namespace api.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonVM, Person>();

            CreateMap<Person, PersonVM>()
                .ForMember(u => u.Company, opt => opt.MapFrom(sp => sp.Company));
        }
    }
}
