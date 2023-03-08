using api.Domain.Services.Interfaces;
using api.Domain.VM.Shared;
using FakeData.PersonData;
using FluentAssertions;
using Manager.VM.Person;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Net;
using WebApi.Controllers;
using Xunit;

namespace WebApi.Tests.Controllers
{
    public class PersonControllerTest
    {
        private readonly IPersonService personService;
        private readonly PersonController controller;
        private readonly List<PersonVM> personVMList;
        private readonly PersonVM personVM;
        private readonly NewPersonVM newPersonVM;

        public PersonControllerTest()
        {
            personService = Substitute.For<IPersonService>();
            controller = new PersonController(personService);

            personVMList = new PersonVMFaker().Generate(10);
            personVM = new PersonVMFaker().Generate();
            newPersonVM = new NewPersonVMFaker().Generate();
        }

        [Fact]
        public async Task Get_Ok()
        {
            var control = new List<PersonVM>();
            personVMList.ForEach(x => control.Add(x.TypedCloneDependency()));
            personService.Get().Returns(personVMList);

            var result = (ObjectResult)await controller.Get();

            await personService.Received().Get();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeEquivalentTo(control);
        }

        [Fact]
        public async Task Get_NotFound()
        {
            personService.Get().Returns(new List<PersonVM>());

            var resultado = (StatusCodeResult)await controller.Get();

            await personService.Received().Get();
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task Post_Created()
        {
            personService.Post(Arg.Any<NewPersonVM>()).Returns(personVM.TypedCloneDependency());

            var resultado = (ObjectResult)await controller.Post(newPersonVM);

            await personService.Received().Post(Arg.Any<NewPersonVM>());
            resultado.StatusCode.Should().Be(StatusCodes.Status201Created);
            resultado.Value.Should().BeEquivalentTo(personVM);
        }

        [Fact]
        public async Task GetWildCard_Ok()
        {
            personService.GetWildCard().Returns(personVM.TypedCloneDependency());

            var resultado = (ObjectResult)await controller.GetWildCard();

            await personService.Received().GetWildCard();
            resultado.Value.Should().BeEquivalentTo(personVM);
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
        }


        [Fact]
        public async Task GetSearch_Ok()
        {
            var control = new List<PersonVM>();
            personVMList.ForEach(x => control.Add(x.TypedCloneDependency()));

            personService.GetWithFilters(personVM.FullName, personVM.PhoneNumber, personVM.Address).Returns(personVMList);
            var result = (ObjectResult)await controller.GetWithFilters(personVM.FullName, personVM.PhoneNumber, personVM.Address);
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeEquivalentTo(control);
        }

        [Fact]
        public async Task Add_Edit_Remove()
        {
            personService.Post(Arg.Any<NewPersonVM>()).Returns(personVM.TypedCloneDependency());
            var postResult = (ObjectResult)await controller.Post(newPersonVM);
            await personService.Received().Post(Arg.Any<NewPersonVM>());
            postResult.StatusCode.Should().Be(StatusCodes.Status201Created);
            postResult.Value.Should().BeEquivalentTo(personVM);

            personService.Put(Arg.Any<PersonVM>()).Returns(personVM.TypedCloneDependency());
            var putResult = (ObjectResult)await controller.Put(personVM);
            await personService.Received().Put(Arg.Any<PersonVM>());
            putResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            putResult.Value.Should().BeEquivalentTo(personVM);

            personService.Delete(Arg.Any<Guid>()).Returns(personVM);
            var deleteResult = (StatusCodeResult)await controller.Delete(personVM.Id);
            await personService.Received().Delete(Arg.Any<Guid>());
            deleteResult.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }
    }
}
