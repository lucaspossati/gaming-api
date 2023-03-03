using api.Domain.VM.Shared;

namespace API.Domain.VM{
    public class PersonVM : BaseViewModel
    {
        public Guid Id { get; set; }

        public string? FullName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public Guid? CompanyId { get; set; }

        public CompanyVM? Company { get; set; }

    }
}