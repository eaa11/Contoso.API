using System.ComponentModel.DataAnnotations;

namespace Contoso.API.Dtos.AddressDtos
{
    public class AddressForUpdateDto
    {
        public string Municipality { get; set; } = string.Empty;

        public string Sector { get; set; } = string.Empty;

        public string StreetName { get; set; } = string.Empty;

        public string ZipCode { get; set; } = string.Empty;

        public string? AddressDescription { get; set; }
    }
}
