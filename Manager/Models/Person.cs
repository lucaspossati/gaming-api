namespace Application.Models
{
    public class Person
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public Guid CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
