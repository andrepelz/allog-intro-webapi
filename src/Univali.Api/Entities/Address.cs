using Univali.Api.Models;

namespace Univali.Api.Entities;

public class Address {
    public int Id { get; set; }
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;

    public bool Equals(AddressCreateDto dto) {
        return (Street == dto.Street && City == dto.City);
    }

    public void Update(AddressUpdateDto newData) {
        Street = newData.Street;
        City = newData.City;
    }

    public void Patch(AddressPatchDto newData) {
        Street = newData.Street;
        City = newData.City;
    }
}
