using api.Domain.Services.Interfaces;
using api.Domain.VM.Shared;
using API.Domain.VM;
using FluentAssertions;
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
        private readonly PersonVM personVM;
        private readonly List<PersonVM> personVMList;
        private readonly BaseResponse<List<PersonVM>> baseResponse;

        public PersonControllerTest()
        {
            personService = Substitute.For<IPersonService>();
            controller = new PersonController(personService);

            baseResponse.Data = personVMList;
        }

        [Fact]
        public async Task Get_Ok()
        {
            var control = new List<PersonVM>();
            //personVMList.ForEach(x => control.Add(x.TypedClone()));

            personService.Get().Returns(personVMList);
            var result = await controller.Get();
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            //result.Data.Should().BeEquivalentTo(control);
        }
    }
}
