using Univali.Api.Models;

namespace Univali.Api.Entities;

public class Customer {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public List<Address> Addresses { get; set; } = new List<Address>();

    public void Update(CustomerUpdateDto newData) {
        this.Name = newData.Name;
        this.Cpf = newData.Cpf;

        var newAddresses = new List<Address>();
        var addresses = Data.Instance.Customers.SelectMany(c => c.Addresses);
        foreach(var addressCreateDto in newData.Addresses) {
            int newAddressId = addresses.Any() ? addresses.Max(c => c.Id) + 1 : 1;

            var newAddress = AddressMapper.Instance.ToAddress(addressCreateDto);
            newAddress.Id = newAddressId;

            newAddresses.Add(newAddress);
        }

        this.Addresses = newAddresses;
    }

    public void Patch(CustomerPatchDto newData) {
        this.Name = newData.Name;
        this.Cpf = newData.Cpf;

        var newAddresses = new List<Address>();
        this.Addresses = newAddresses;
        
        var addresses = Data.Instance.Customers.SelectMany(c => c.Addresses);
        foreach(var addressDto in newData.Addresses) {
            int newAddressId = addresses.Any() ? addresses.Max(c => c.Id) + 1 : 1;

            var newAddress = AddressMapper.Instance.ToAddress(addressDto);
            if(newAddress.Id == 0)
                newAddress.Id = newAddressId;

            newAddresses.Add(newAddress);
        }
    }
}
