using api.Domain.VM.Shared;

namespace API.Domain.VM{
    public class PersonVM : BaseViewModel, ICloneable
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public Guid CompanyId { get; set; }

        public CompanyVM Company { get; set; }

        public object Clone()
        {
            return (PersonVM)MemberwiseClone();
        }

        public object CloneWithDependency()
        {
            var person = (PersonVM)MemberwiseClone();
            person.Company = (CompanyVM)person.Company.Clone();

            return person;
        }

        public PersonVM TypedClone()
        {
            return (PersonVM)Clone();
        }

        public PersonVM TypedCloneDependency()
        {
            return (PersonVM)CloneWithDependency();
        }

    }
}