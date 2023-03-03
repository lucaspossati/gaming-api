using API.Domain.VM;

namespace api.Domain.Services.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonVM>> Get();
        Task<PersonVM> GetWildCard();
        Task<PersonVM> Get(Guid id);
        Task<PersonVM> Post(PersonVM model);
        Task<PersonVM> Put(PersonVM model);
        Task<PersonVM> Delete(Guid id);
    }
}
