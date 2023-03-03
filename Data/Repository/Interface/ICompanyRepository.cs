
using Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repository.Interface
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> Get();

        Task<Company?> Get(Guid id);

        Task<Company?> Get(string name, Guid? id = null);

        Task<Company> Post(Company model);

        Task<Company> Put(Company model);

        Task Delete(Guid id);
    }
}
