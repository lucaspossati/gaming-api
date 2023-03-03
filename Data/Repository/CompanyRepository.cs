using Application.Models;
using Data.Context;
using Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataContext context;

        public CompanyRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Company>> Get()
        {
            return await context.Companies
                .Include(x => x.People)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<Company?> Get(string name, Guid? id = null)
        {
            if(id != null)
            {
                var company = await context.Companies.FirstOrDefaultAsync(x => x.Name == name && x.Id != id);
                return company;
            }
            else
            {
                var company = await context.Companies.FirstOrDefaultAsync(x => x.Name == name);
                return company;
            }
            
        }

        public async Task<Company?> Get(Guid id)
        {
            return await context.Companies.FindAsync(id);
        }

        public async Task<Company> Post(Company model)
        {
            context.Companies.Add(model);

            await context.SaveChangesAsync();

            return model;
        }

        public async Task<Company> Put(Company model)
        {
            context.Entry(model).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return model;
        }

        public async Task Delete(Guid id)
        {
            var model = await context.Companies.FindAsync(id);

            if (model == null) return;

            context.Companies.Remove(model);

            await context.SaveChangesAsync();
        }
    }
}
