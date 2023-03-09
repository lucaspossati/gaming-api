using api.Domain.Services;
using api.Domain.Services.Interfaces;
using api.Profiles;
using Application.Models;
using AutoMapper;
using Data.Repository.Interface;
using FakeData.PersonData;
using FluentAssertions;
using Manager.VM.Person;
using NSubstitute;
using Xunit;

namespace Manager.Tests.Services
{
    public class PersonServicesTest
    {
        private readonly IPersonRepository personRepository;
        private readonly IPersonService service;
        private readonly IMapper mapper;
        private readonly List<Person> personList;
        private readonly Person person;
        private readonly PersonVM personVM;
        private readonly NewPersonVM newPersonVM;

        public PersonServicesTest()
        {
            personRepository = Substitute.For<IPersonRepository>();
            mapper = new MapperConfiguration(p => p.AddProfile<PersonProfile>()).CreateMapper();
            service = new PersonService(mapper, personRepository);

            personList = new PersonFaker().Generate(10);
            person = new PersonFaker().Generate();
            newPersonVM = new NewPersonVMFaker().Generate();
            personVM = new PersonVMFaker(true).Generate();
        }

        [Fact]
        public async Task Get_Success()
        {
            personRepository.Get().Returns(personList);
            var control = mapper.Map<IEnumerable<PersonVM>>(personList);
            var response = await service.Get();

            await personRepository.Received().Get();
            response.Should().BeEquivalentTo(control);
        }

        [Fact]
        public async Task Get_Empty()
        {
            personRepository.Get().Returns(new List<Person>());

            var retorno = await service.Get();

            await personRepository.Received().Get();
            retorno.Should().BeEquivalentTo(new List<Person>());
        }

        [Fact]
        public async Task GetWildCard_Success()
        {
            personRepository.Get().Returns(personList);
            var randomIndex = await service.GenerateRandomIndex();
            var control = mapper.Map<PersonVM>(personList[randomIndex]);
            var response = await service.GetWildCard(randomIndex);

            await personRepository.Received().Get();
            response.Should().BeEquivalentTo(control);
        }


        [Fact]
        public async Task GetSearch_Ok()
        {
            personRepository.GetWithFilters(person.FullName, person.PhoneNumber, person.Address).Returns(personList);
            var control = mapper.Map<IEnumerable<PersonVM>>(personList);
            var response = await service.GetWithFilters(person.FullName, person.PhoneNumber, person.Address);

            await personRepository.Received().GetWithFilters(person.FullName, person.PhoneNumber, person.Address);
            response.Should().BeEquivalentTo(control);
        }

        [Fact]
        public async Task Post_Created()
        {
            personRepository.Post(Arg.Any<Person>()).Returns(person);
            var control = mapper.Map<PersonVM>(person);
            var response = await service.Post(newPersonVM);

            await personRepository.Received().Post(Arg.Any<Person>());
            response.Should().BeEquivalentTo(control);
        }

    }
}
