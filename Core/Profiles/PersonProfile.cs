using Application.Models;
using AutoMapper;
using Manager.VM.Person;

namespace api.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonVM, Person>();

            CreateMap<NewPersonVM, PersonVM>();

            CreateMap<Person, PersonVM>()
                .ForMember(u => u.Company, opt => opt.MapFrom(sp => sp.Company));


        }
    }
}
