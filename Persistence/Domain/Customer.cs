namespace Contoso.API.Persistence.Domain
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string IdentificationCard { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public ICollection<Address> Addresses { get; set; }

        public string RegisterDate { get; set; } = DateTime.UtcNow.ToShortDateString();

        public Customer()
        {
            Addresses = new List<Address>();
        }
    }
}