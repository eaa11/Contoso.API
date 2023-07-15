namespace Contoso.API.Persistence.Domain
{
    public class Address
    {
        public int Id { get; set; }

        public string Municipality { get; set; } = string.Empty;

        public string Sector { get; set; } = string.Empty;

        public string StreetName { get; set; } = string.Empty;

        public string ZipCode { get; set; } = string.Empty;

        public string? AddressDescription { get; set; }

        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }
    }
}