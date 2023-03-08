using api.Domain.Services;
using api.Domain.Services.Interfaces;
using api.Profiles;
using Application.Models;
using AutoMapper;
using Data.Repository.Interface;
using FakeData.CompanyData;
using FluentAssertions;
using Manager.VM.Company;
using NSubstitute;
using Xunit;

namespace Manager.Tests.Services
{
    public class CompanyServicesTest
    {
        private readonly ICompanyRepository companyRepository;
        private readonly ICompanyService service;
        private readonly IMapper mapper;
        private readonly List<Company> companyList;
        private readonly Company company;
        private readonly NewCompanyVM newCompanyVM;

        public CompanyServicesTest()
        {
            companyRepository = Substitute.For<ICompanyRepository>();
            mapper = new MapperConfiguration(p => p.AddProfile<CompanyProfile>()).CreateMapper();
            service = new CompanyService(mapper, companyRepository);

            companyList = new CompanyFaker().Generate(10);
            company = new CompanyFaker().Generate();
            newCompanyVM = new NewCompanyVMFaker().Generate();
        }

        [Fact]
        public async Task Get_Success()
        {
            companyRepository.Get().Returns(companyList);
            var control = mapper.Map<IEnumerable<CompanyVM>>(companyList);
            var response = await service.Get();

            await companyRepository.Received().Get();
            response.Should().BeEquivalentTo(control);
        }

        [Fact]
        public async Task Get_Empty()
        {
            companyRepository.Get().Returns(new List<Company>());

            var retorno = await service.Get();

            await companyRepository.Received().Get();
            retorno.Should().BeEquivalentTo(new List<Company>());
        }

        [Fact]
        public async Task Post_Created()
        {
            companyRepository.Post(Arg.Any<Company>()).Returns(company);
            var controle = mapper.Map<CompanyVM>(company);
            var retorno = await service.Post(newCompanyVM);

            await companyRepository.Received().Post(Arg.Any<Company>());
            retorno.Should().BeEquivalentTo(controle);
            
        }
       
    }
}
