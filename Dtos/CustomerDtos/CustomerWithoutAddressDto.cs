namespace Contoso.API.Dtos.CustomerDtos
{
    public class CustomerWithoutAddressDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string IdentificationCard { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string FullName => $"{Name} {LastName}";

        public string RegisterDate { get; set; } = string.Empty;
    }
}