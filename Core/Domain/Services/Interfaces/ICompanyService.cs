using API.Domain.VM;

namespace api.Domain.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyVM>> Get();
        Task<CompanyVM> Get(Guid id);
        Task<CompanyVM> Post(CompanyVM model);
        Task<CompanyVM> Put(CompanyVM model);
        Task<CompanyVM> Delete(Guid id);
    }
}
