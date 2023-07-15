using AutoMapper;
using Contoso.API.Dtos.CustomerDtos;
using Contoso.API.Persistence.Domain;
using Contoso.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Contoso.API.Controller
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerWithoutAddressDto>>> GetCustomers()
        {
            var customers = await _customerRepository.GetCustomersAsync();

            return Ok(_mapper.Map<IEnumerable<CustomerWithoutAddressDto>>(customers));
        }

        [HttpGet("{customerId}", Name = "GetCustomer")]
        public async Task<IActionResult> GetCustomer(int customerId, bool includeAddress = false)
        {
            var customer = await _customerRepository.GetCustomerAsync(customerId, includeAddress);

            if (customer == null) return NotFound();

            if (includeAddress)
            {
                return Ok(_mapper.Map<CustomerDto>(customer));
            }
            return Ok(_mapper.Map<CustomerWithoutAddressDto>(customer));
        }

        [HttpPost()]
        public async Task<ActionResult<CustomerDto>> CreateCustomer(CustomerForCreationDto customerForCreationDto)
        {
            var newCustomer = _mapper.Map<Customer>(customerForCreationDto);

            _customerRepository.AddCustomer(newCustomer);

            await _customerRepository.SaveChangesAsync();

            var customerCrated = _mapper.Map<CustomerDto>(newCustomer);

            return CreatedAtRoute("GetCustomer",
                new
                {
                    customerId = newCustomer.Id,
                },
                customerCrated);
        }

        [HttpPut("{customerId}")]
        public async Task<ActionResult> UpdateCustomer(int customerId, CustomerForUpdateDto customerForUpdateDto)
        {
            if (!await _customerRepository.HasCustomersAsync(customerId))
                return NotFound();

            var customerFound = await _customerRepository.GetCustomerAsync(customerId, false);

            if (customerFound == null) return NotFound();

            _mapper.Map(customerForUpdateDto, customerFound);

            await _customerRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{customerId}")]
        public async Task<ActionResult> PartialCustomerUpdate(int customerId,
            JsonPatchDocument<CustomerForUpdateDto> customerPatch)
        {
            if (!await _customerRepository.HasCustomersAsync(customerId))
                return NotFound();

            var customerFound = await _customerRepository
                .GetCustomerAsync(customerId, false);

            if (customerFound == null) return NotFound();

            var customerToPatch = _mapper.Map<CustomerForUpdateDto>(customerFound);

            customerPatch.ApplyTo(customerToPatch, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!TryValidateModel(customerToPatch)) return BadRequest(ModelState);

            _mapper.Map(customerToPatch, customerFound);

            await _customerRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{customerId}")]
        public async Task<ActionResult> DeleteAddress(int customerId)
        {
            if (!await _customerRepository.HasCustomersAsync(customerId))
                return NotFound();

            var customerFound = await _customerRepository
               .GetCustomerAsync(customerId, false);

            if (customerFound == null) return NotFound();

            _customerRepository.DeleteCustomer(customerFound);

            await _customerRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}