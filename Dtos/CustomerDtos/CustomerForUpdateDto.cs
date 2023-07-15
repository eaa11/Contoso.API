namespace Contoso.API.Dtos.CustomerDtos
{
    public class CustomerForUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string IdentificationCard { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
