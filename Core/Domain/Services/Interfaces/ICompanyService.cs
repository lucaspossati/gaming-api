using Manager.VM.Company;

namespace api.Domain.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyVM>> Get();
        Task<CompanyVM> Post(NewCompanyVM model);
    }
}
