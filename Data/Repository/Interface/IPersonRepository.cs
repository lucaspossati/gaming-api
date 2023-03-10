
using Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repository.Interface
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> Get();

        Task<IEnumerable<Person>> GetWithFilters(string? fullName = null, string? phoneNumber = null, string? address = null);

        Task<Person?> Get(Guid id);

        Task<Person> Post(Person model);

        Task<Person> Put(Person model);

        Task Delete(Guid id);

        Person GetWildCard();
    }
}
