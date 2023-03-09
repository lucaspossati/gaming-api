using Application.Models;
using Data.Context;
using Data.Repository;
using Data.Repository.Interface;
using FakeData.PersonData;
using FluentAssertions;
using Manager.VM.Person;
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
    public class PersonRepositoryTest : IDisposable
    {
        private readonly IPersonRepository repository;
        private readonly DataContext context;
        private readonly Person person;
        private readonly PersonFaker personFaker;

        public PersonRepositoryTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseInMemoryDatabase("Db_test");

            context = new DataContext(optionsBuilder.Options);
            repository = new PersonRepository(context);

            personFaker = new PersonFaker(true);
            person = personFaker.Generate();
        }

        [Fact]
        public async Task Get_Success()
        {
            var persons = await InsertPersons();
            var response = await repository.Get();

            response.Should().HaveCount(persons.Count);
            response.First().Company.Should().NotBeNull();
        }

        [Fact]
        public async Task Get_Empty()
        {
            var response = await repository.Get();

            response.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetWithFilters_Success()
        {
            var persons = await InsertPersons();
            var person = persons.First();
            var response = await repository.GetWithFilters(person.FullName, person.PhoneNumber, person.Address);

            response.Should().HaveCount(1);
            response.First().Company.Should().NotBeNull();
        }

        [Fact]
        public async Task Post_Created()
        {
            var response = await repository.Post(person);

            response.Should().BeEquivalentTo(person);
        }

        private async Task<List<Person>> InsertPersons()
        {
            var persons = personFaker.Generate(100);
            persons.ForEach(async x => await context.People.AddAsync(x));
            await context.SaveChangesAsync();
            return persons;
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
        }
    }
}
