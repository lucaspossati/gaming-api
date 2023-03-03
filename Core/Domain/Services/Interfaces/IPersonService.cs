using API.Domain.VM;

namespace api.Domain.Services.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonVM>> Get();
        Task<IEnumerable<PersonVM>> GetWithFilters(string? fullName = null, string? phoneNumber = null, string? address = null);
        Task<PersonVM> GetWildCard();
        Task<PersonVM> Get(Guid id);
        Task<PersonVM> Post(PersonVM model);
        Task<PersonVM> Put(PersonVM model);
        Task<PersonVM> Delete(Guid id);
    }
}
