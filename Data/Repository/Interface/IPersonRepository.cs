
using Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repository.Interface
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> Get();

        Task<Person?> Get(Guid id);

        Task<Person> Post(Person model);

        Task<Person> Put(Person model);

        Task Delete(Guid id);
    }
}
