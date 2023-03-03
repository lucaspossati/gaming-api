using api.Domain.VM.Shared;

namespace API.Domain.VM{
    public class CompanyVM : BaseViewModel
    {
        public CompanyVM()
        {
            setNumberOfPersons(People != null ? People.Count : 0);
        }

        public Guid Id { get; set; }

        public string? Name { get; set; }

        public int? NumberOfPersonsLinked { get; private set; }

        public DateTime? RegistrationDate { get; set; }

        public ICollection<PersonVM>? People { get; set; }

        public void setNumberOfPersons(int? numberOfPersons)
        {
            NumberOfPersonsLinked = numberOfPersons;
        }
    }
}