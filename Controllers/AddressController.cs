using AutoMapper;
using Contoso.API.Dtos.AddressDtos;
using Contoso.API.Persistence.Domain;
using Contoso.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Contoso.API.Controllers
{
    [Route("api/customers/{customerId}/addresses")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly ILogger _logger;

        private readonly ICustomerRepository _customerRepository;

        private readonly IMapper _mapper;

        public AddressController(ILogger<AddressController> logger,
            ICustomerRepository customerRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetAddresses(int customerId)
        {
            if (!await _customerRepository.HasCustomersAsync(customerId))
            {
                _logger.LogInformation($"Customer with {customerId} was not found when getting the address.");
                return NotFound();
            }
            var customerAddress = await _customerRepository
                .GetAddressesForCustomerAsync(customerId);

            return Ok(_mapper.Map<IEnumerable<AddressDto>>(customerAddress));
        }

        [HttpGet("{addressId}", Name = "GetAddress")]
        public async Task<ActionResult<AddressDto>> GetAddress(int customerId, int addressId)
        {
            if (!await _customerRepository.HasCustomersAsync(customerId))
                return NotFound();

            var customerAddress = await _customerRepository
                .GetAddressForCustomerAsync(customerId, addressId);

            if (customerAddress == null) return NotFound();

            return Ok(_mapper.Map<AddressDto>(customerAddress));
        }

        [HttpPost()]
        public async Task<ActionResult<AddressDto>> CreateAddress(int customerId, AddressForCreationDto addressForCreationDto)
        {
            if (!await _customerRepository.HasCustomersAsync(customerId)) return NotFound();

            var newAddress = _mapper.Map<Address>(addressForCreationDto);

            await _customerRepository.AddAddressForCustomerAsync(customerId, newAddress);

            await _customerRepository.SaveChangesAsync();

            var addressCreated = _mapper.Map<AddressDto>(newAddress);

            return CreatedAtRoute("GetAddress",
                new
                {
                    Id = customerId,
                    addressId = newAddress.Id
                },
                addressCreated);
        }

        [HttpPut("{addressId}")]
        public async Task<ActionResult> UpdateAddress(int customerId, int addressId, AddressForUpdateDto addressForUpdateDto)
        {
            if (!await _customerRepository.HasCustomersAsync(customerId))
                return NotFound();

            var addressFound = await _customerRepository.GetAddressForCustomerAsync(customerId, addressId);

            if (addressFound == null) return NotFound();

            _mapper.Map(addressForUpdateDto, addressFound);

            await _customerRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{addressId}")]
        public async Task<ActionResult> PartialUpdateAddress(int customerId, int addressId,
            JsonPatchDocument<AddressForUpdateDto> addressPatch)
        {
            if (!await _customerRepository.HasCustomersAsync(customerId))
                return NotFound();

            var addressFound = await _customerRepository
                .GetAddressForCustomerAsync(customerId, addressId);

            if (addressFound == null) return NotFound();

            var addressToPatch = _mapper.Map<AddressForUpdateDto>(addressFound);

            addressPatch.ApplyTo(addressToPatch, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!TryValidateModel(addressToPatch)) return BadRequest(ModelState);

            _mapper.Map(addressToPatch, addressFound);

            await _customerRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{addressId}")]
        public async Task<ActionResult> DeleteAddress(int customerId, int addressId)
        {
            if (!await _customerRepository.HasCustomersAsync(customerId))
                return NotFound();

            var addressFound = await _customerRepository
               .GetAddressForCustomerAsync(customerId, addressId);

            if (addressFound == null) return NotFound();

            _customerRepository.DeleteAddressForCustomer(addressFound);

            await _customerRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}