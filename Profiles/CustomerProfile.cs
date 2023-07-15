using AutoMapper;
using Contoso.API.Dtos.CustomerDtos;
using Contoso.API.Persistence.Domain;

namespace Contoso.API.Profiles
{
    public class CustomerProfile:Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerWithoutAddressDto>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerForCreationDto, Customer>();
            CreateMap<CustomerForUpdateDto, Customer>();
            CreateMap<Customer, CustomerForUpdateDto>();
        }
    }
}
