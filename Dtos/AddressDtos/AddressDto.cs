namespace Contoso.API.Dtos.AddressDtos
{
    public class AddressDto
    {
        public int Id { get; set; }

        public string Municipality { get; set; } = string.Empty;

        public string Sector { get; set; } = string.Empty;

        public string StreetName { get; set; } = string.Empty;

        public string ZipCode { get; set; } = string.Empty;

        public string? AddressDescription { get; set; }

        public string FullAddress
        {
            get
            {
                return $"St/ {StreetName}, " +
                       $"sector: {Sector}, " +
                       $"municipio: {Municipality}, " +
                       $"codigo postal: {ZipCode} " +
                       $"Descripcion: {AddressDescription}";
            }
        }
    }
}