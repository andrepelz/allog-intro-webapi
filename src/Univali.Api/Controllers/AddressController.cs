using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Univali.Api.Entities;
using Univali.Api.Models;

namespace Univali.Api.Controllers;

[ApiController]
[Route("api/customers/{customerId}/addresses")]
public class AddressController : ControllerBase {
    private AddressMapper _mapper = AddressMapper.Instance;

    

    [HttpGet]
    public ActionResult<IEnumerable<AddressDto> > GetAddresses(int customerId) {
        var customerFromDatabase = Data.Instance.Customers
            .FirstOrDefault(c => c.Id == customerId);

        if(customerFromDatabase != null) {
            var addressesToReturn = customerFromDatabase.Addresses
                .Select(a => _mapper.ToDto(a));

            return Ok(addressesToReturn);
        }

        return NotFound();
    }

    [HttpGet("{addressId}", Name = "GetAddressById")]
    public ActionResult<AddressDto> GetAddressById(int customerId, int addressId) {
        var addressFromDatabase = Data.Instance
            .Customers.FirstOrDefault(c => c.Id == customerId)
            ?.Addresses.FirstOrDefault(a => a.Id == addressId);

        if(addressFromDatabase != null) {
            var addressesToReturn = _mapper.ToDto(addressFromDatabase);

            return Ok(addressesToReturn);
        }

        return NotFound();
    }

    [HttpPost]
    public ActionResult<AddressDto> CreateAddress (int customerId, AddressCreateDto addressCreateDto) {
        var customerFromDatabase = Data.Instance.Customers
            .FirstOrDefault(c => c.Id == customerId);

        if(customerFromDatabase != null) {
            var addresses = Data.Instance.Customers.SelectMany(c => c.Addresses);
            int newAddressId = addresses.Any() ? addresses.Max(c => c.Id) + 1 : 1;

            var newAddress = _mapper.ToAddress(addressCreateDto);
            newAddress.Id = newAddressId;

            customerFromDatabase.Addresses.Add(newAddress);
            
            var addressToReturn = _mapper.ToDto(newAddress);

            return CreatedAtRoute(
                "GetAddressById",
                new { 
                    customerId = customerFromDatabase.Id,
                    addressId = addressToReturn.Id 
                },
                addressToReturn
            );
        }
        
        return NotFound();
    }

    [HttpPut("{addressId}")]
    public ActionResult<AddressDto> UpdateAddress (int customerId, int addressId, AddressUpdateDto addressUpdateDto) {
        var addressFromDatabase = Data.Instance
            .Customers.FirstOrDefault(c => c.Id == customerId)
            ?.Addresses.FirstOrDefault(a => a.Id == addressId);

        if(addressFromDatabase != null) {
            addressFromDatabase.Update(addressUpdateDto);
            
            var addressToReturn = _mapper.ToDto(addressFromDatabase);

            return NoContent();
        }
        
        return NotFound();
    }

    [HttpDelete("{addressId}")]
    public ActionResult DeleteAddress(int customerId, int addressId) {
        var addresses = Data.Instance.Customers
            .FirstOrDefault(c => c.Id == customerId)?.Addresses;
        
        bool success = false;
        if(addresses != null) {
            var addressFromDatabase = addresses.FirstOrDefault(a => a.Id == addressId);

            success = addresses.Remove(addressFromDatabase);
        }

        return success ? NoContent() : NotFound();
    }

    [HttpPatch("{addressId}")]
    public ActionResult PartiallyUpdateCustomer(
        [FromBody] JsonPatchDocument<AddressPatchDto> patchDocument, 
        [FromRoute] int customerId,
        [FromRoute] int addressId
    ) {
        var addressFromDatabase = Data.Instance
            .Customers.FirstOrDefault(c => c.Id == customerId)
            ?.Addresses.FirstOrDefault(a => a.Id == addressId);

        if (addressFromDatabase != null) {
            var addressToPatch = _mapper.ToPatchDto(addressFromDatabase);

            patchDocument.ApplyTo(addressToPatch);

            addressFromDatabase.Patch(addressToPatch);

            return NoContent();
        }

        return NotFound();
    }
}
