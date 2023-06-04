using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Univali.Api.Entities;
using Univali.Api.Models;

namespace Univali.Api.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase {
    internal CustomerMapper _mapper = CustomerMapper.Instance;

    

    [HttpGet]
    public ActionResult<IEnumerable<CustomerDto> > GetCustomers() {
        var customers = Data.Instance.Customers;
        var customersToReturn = customers.Select(c => _mapper.ToDto(c));

        return Ok(customersToReturn);
    }

    [HttpGet("with-addresses")]
    public ActionResult<IEnumerable<CustomerDto> > GetCustomersWithAddresses() {
        var customers = Data.Instance.Customers;
        var customersToReturn = customers.Select(c => _mapper.ToWithAddressesDto(c));

        return Ok(customersToReturn);
    }

    [HttpGet("{id}", Name = "GetCustomerById")]
    public ActionResult<CustomerDto> GetCustomerById(int id) {
        var customerFromDatabase = Data.Instance
            .Customers.FirstOrDefault(c => c.Id == id);

        var customerToReturn = _mapper.ToDto(customerFromDatabase);

        return customerToReturn != null ? Ok(customerToReturn) : NotFound();
    }

    [HttpGet("cpf/{cpf}", Name = "GetCustomerByCpf")]
    public ActionResult<CustomerDto> GetCustomerByCpf(string cpf) {
        var customerFromDatabase = Data.Instance
            .Customers.FirstOrDefault(c => c.Cpf == cpf);

        var customerToReturn = _mapper.ToDto(customerFromDatabase);

        return customerToReturn != null ? Ok(customerToReturn) : NotFound();
    }

    [HttpPost]
    public ActionResult<CustomerDto> CreateCustomer(CustomerCreateDto customerCreateDto) {
        var customers = Data.Instance.Customers;
        int newCustomerId = customers.Any() ? customers.Max(c => c.Id) + 1 : 1;

        var newCustomer = _mapper.ToCustomer(customerCreateDto);
        newCustomer.Id = newCustomerId;

        customers.Add(newCustomer);
        
        var customerToReturn = _mapper.ToDto(newCustomer);

        return CreatedAtRoute(
            "GetCustomerById",
            new { Id = customerToReturn.Id },
            customerToReturn
        );
    }

    [HttpPost("with-addresses")]
    public ActionResult<CustomerWithAddressesDto> CreateCustomerWithAddresses(CustomerWithAddressesCreateDto customerWithAddressesCreateDto) {
        var customers = Data.Instance.Customers;
        int newCustomerId = customers.Any() ? customers.Max(c => c.Id) + 1 : 1;

        var newCustomer = _mapper.ToCustomer(customerWithAddressesCreateDto);
        newCustomer.Id = newCustomerId;

        customers.Add(newCustomer);

        foreach(var addressCreateDto in customerWithAddressesCreateDto.Addresses) {
            var addresses = customers.SelectMany(c => c.Addresses);
            int newAddressId = addresses.Any() ? addresses.Max(c => c.Id) + 1 : 1;

            var newAddress = AddressMapper.Instance.ToAddress(addressCreateDto);
            newAddress.Id = newAddressId;

            newCustomer.Addresses.Add(newAddress);
        }
        
        var customerToReturn = _mapper.ToWithAddressesDto(newCustomer);

        return CreatedAtRoute(
            "GetCustomerById",
            new { Id = customerToReturn.Id },
            customerToReturn
        );
    }

    [HttpPut("{id}")]
    public ActionResult UpdateCustomer(int id, CustomerUpdateDto customerUpdateDto) {
        var customerFromDatabase = Data.Instance.Customers.FirstOrDefault(c => c.Id == id);

        if(customerFromDatabase != null) {
            customerFromDatabase.Update(customerUpdateDto);
            
            var customerToReturn = _mapper.ToDto(customerFromDatabase);

            return NoContent();
        }

        return NotFound();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCustomer(int id) {
        var customers = Data.Instance.Customers;
        var customerFromDatabase = customers.FirstOrDefault(c => c.Id == id);

        bool success = customers.Remove(customerFromDatabase);

        return success ? NoContent() : NotFound();
    }

    [HttpPatch("{id}")]
    public ActionResult PartiallyUpdateCustomer(
        [FromBody] JsonPatchDocument<CustomerPatchDto> patchDocument, 
        [FromRoute] int id
    ) {
        var customerFromDatabase = Data.Instance.Customers
            .FirstOrDefault(c => c.Id == id);

        if (customerFromDatabase != null) {
            var customerToPatch = _mapper.ToPatchDto(customerFromDatabase);

            patchDocument.ApplyTo(customerToPatch);

            customerFromDatabase.Patch(customerToPatch);

            return NoContent();
        }

        return NotFound();
    }
}
