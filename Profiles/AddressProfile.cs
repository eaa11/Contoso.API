using AutoMapper;
using Contoso.API.Dtos.AddressDtos;
using Contoso.API.Persistence.Domain;

namespace Contoso.API.Profiles
{
    public class AddressProfile:Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDto>();
            CreateMap<AddressForCreationDto, Address>();
            CreateMap<AddressForUpdateDto, Address>();
            CreateMap<Address, AddressForUpdateDto>();
        }
    }
}
