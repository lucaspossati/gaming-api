using Application.Models;
using Data.Context;
using Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DataContext context;

        public PersonRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Person>> Get()
        {
            return await context.People
                .Include(x => x.Company)
                .OrderBy(x => x.FullName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Person>> GetWithFilters(string? fullName = null, string? phoneNumber = null, string? address = null)
        {
            var queryable = context.People.AsQueryable();

            if (!string.IsNullOrEmpty(fullName))
            {
                queryable = queryable.Where(x => x.FullName == fullName);
            }

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                queryable = queryable.Where(x => x.PhoneNumber == phoneNumber);
            }

            if (!string.IsNullOrEmpty(address))
            {
                queryable = queryable.Where(x => x.Address == address);
            }

            return await queryable.Include(x => x.Company).ToListAsync();
        }

        public async Task<Person?> Get(Guid id)
        {
            return await context.People.FindAsync(id);
        }

        public async Task<Person> Post(Person model)
        {
            context.People.Add(model);

            await context.SaveChangesAsync();

            return model;
        }

        public async Task<Person> Put(Person model)
        {
            context.Entry(model).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return model;
        }

        public async Task Delete(Guid id)
        {
            var model = await context.People.FindAsync(id);

            if (model == null) return;

            context.People.Remove(model);

            await context.SaveChangesAsync();
        }
    }
}
