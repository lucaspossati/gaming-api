using Manager.VM.Person;

namespace api.Domain.Services.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonVM>> Get();
        Task<IEnumerable<PersonVM>> GetWithFilters(string? fullName = null, string? phoneNumber = null, string? address = null);
        Task<PersonVM> GetWildCard(int? index = null);
        Task<PersonVM> Get(Guid id);
        Task<PersonVM> Post(NewPersonVM model);
        Task<PersonVM> Put(PersonVM model);
        Task<PersonVM> Delete(Guid id);
        Task<int> GenerateRandomIndex();
    }
}
