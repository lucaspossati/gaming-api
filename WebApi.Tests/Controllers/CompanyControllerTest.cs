using api.Domain.Services;
using api.Domain.Services.Interfaces;
using api.Domain.VM.Shared;
using FakeData.CompanyData;
using FluentAssertions;
using Manager.VM.Company;
using Manager.VM.Person;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Net;
using WebApi.Controllers;
using Xunit;

namespace WebApi.Tests.Controllers
{
    public class CompanyControllerTest
    {
        private readonly ICompanyService companyService;
        private readonly CompanyController controller;
        private readonly List<CompanyVM> companyVMList;
        private readonly CompanyVM companyVM;
        private readonly NewCompanyVM newCompanyVM;

        public CompanyControllerTest()
        {
            companyService = Substitute.For<ICompanyService>();
            controller = new CompanyController(companyService);

            companyVMList = new CompanyVMFaker().Generate(10);
            companyVM = new CompanyVMFaker().Generate();
            newCompanyVM = new NewCompanyVMFaker().Generate();
        }

        [Fact]
        public async Task Get_Ok()
        {
            var control = new List<CompanyVM>();
            companyVMList.ForEach(x => control.Add(x.TypedClone()));

            companyService.Get().Returns(companyVMList);
            var result = (ObjectResult)await controller.Get();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeEquivalentTo(control);
        }

        [Fact]
        public async Task Get_NotFound()
        {
            companyService.Get().Returns(new List<CompanyVM>());

            var resultado = (StatusCodeResult)await controller.Get();

            await companyService.Received().Get();
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task Post_Created()
        {
            companyService.Post(Arg.Any<NewCompanyVM>()).Returns(companyVM.TypedClone());

            var resultado = (ObjectResult)await controller.Post(newCompanyVM);

            await companyService.Received().Post(Arg.Any<NewCompanyVM>());
            resultado.StatusCode.Should().Be(StatusCodes.Status201Created);
            resultado.Value.Should().BeEquivalentTo(companyVM);
        }

    }
}
