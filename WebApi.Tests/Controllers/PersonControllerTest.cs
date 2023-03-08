﻿using api.Domain.Services.Interfaces;
using api.Domain.VM.Shared;
using API.Domain.VM;
using FakeData.PersonData;
using FluentAssertions;
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

        public PersonControllerTest()
        {
            try
            {
                personService = Substitute.For<IPersonService>();
                controller = new PersonController(personService);

                personVMList = new PersonVMFaker().Generate(10);
                personVM = new PersonVMFaker().Generate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        [Fact]
        public async Task Get_Ok()
        {
            var control = new List<PersonVM>();
            personVMList.ForEach(x => control.Add(x.TypedCloneDependency()));

            personService.Get().Returns(personVMList);
            var result = (ObjectResult)await controller.Get();
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

    }
}
