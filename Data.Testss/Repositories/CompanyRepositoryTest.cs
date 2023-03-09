using Application.Models;
using Data.Context;
using Data.Repository;
using Data.Repository.Interface;
using FakeData.CompanyData;
using FluentAssertions;
using Manager.VM.Company;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Data.Tests.Repositories
{
    public class CompanyRepositoryTest : IDisposable
    {
        private readonly ICompanyRepository repository;
        private readonly DataContext context;
        private readonly Company company;
        private readonly CompanyFaker companyFaker;

        public CompanyRepositoryTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseInMemoryDatabase("Db_test");

            context = new DataContext(optionsBuilder.Options);
            repository = new CompanyRepository(context);

            companyFaker = new CompanyFaker();
            company = companyFaker.Generate();
        }

        [Fact]
        public async Task Get_Success()
        {
            var Companys = await InsertCompanies();
            var response = await repository.Get();

            response.Should().HaveCount(Companys.Count);
            response.First().People.Should().NotBeNull();
        }

        [Fact]
        public async Task Get_Empty()
        {
            var response = await repository.Get();

            response.Should().HaveCount(0);
        }

        [Fact]
        public async Task Post_Created()
        {
            var response = await repository.Post(company);

            response.Should().BeEquivalentTo(company);
        }

        private async Task<List<Company>> InsertCompanies()
        {
            var companies = companyFaker.Generate(100);
            companies.ForEach(async x => await context.Companies.AddAsync(x));
            await context.SaveChangesAsync();
            return companies;
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
        }
    }
}
