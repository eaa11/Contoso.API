using Contoso.API.Dtos.AddressDtos;

namespace Contoso.API.Dtos.CustomerDtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string IdentificationCard { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public ICollection<AddressDto> Addresses { get; set; }

        public string RegisterDate { get; set; }

        public string FullName => $"{Name} {LastName}";

        public CustomerDto()
        {
            Addresses = new List<AddressDto>();
        }
    }
}