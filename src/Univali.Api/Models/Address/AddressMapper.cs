using Univali.Api.Entities;

namespace Univali.Api.Models;

/// <summary> Singleton DTO mapper for Address </summary>
public class AddressMapper { 
    private static AddressMapper? _instance;

    private AddressMapper() {}



    public AddressDto? ToDto(Address? a) {
        if(a != null) {
            var newAddressDto = new AddressDto {
                Id = a.Id,
                Street = a.Street,
                City = a.City
            };

            return newAddressDto;
        }

        return null;
    }

    public AddressCreateDto ToCreateDto(Address a) {
        return new AddressCreateDto { Street = a.Street, City = a.City };
    }

    public AddressPatchDto ToPatchDto(Address a) {
        return new AddressPatchDto { Street = a.Street, City = a.City };
    }
    
    public Address ToAddress(AddressDto dto) {
        return new Address { Id = dto.Id, Street = dto.Street, City = dto.City };
    }

    public Address ToAddress(AddressCreateDto dto) {
        return new Address { Street = dto.Street, City = dto.City };
    }



    public static AddressMapper Instance { 
        get { return _instance ??= new AddressMapper(); } 
    }
}
