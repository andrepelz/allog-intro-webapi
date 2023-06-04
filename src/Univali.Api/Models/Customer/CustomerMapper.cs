using Univali.Api.Entities;

namespace Univali.Api.Models;

/// <summary> Singleton DTO mapper for Customer </summary>
public class CustomerMapper { 
    private static CustomerMapper? _instance;

    private CustomerMapper() {}



    public CustomerDto? ToDto(Customer? c) {
        if(c != null) {
            var newCustomerDto = new CustomerDto {
                Id = c.Id,
                Name = c.Name,
                Cpf = c.Cpf
            };

            return newCustomerDto;
        }

        return null;
    }

    public CustomerWithAddressesDto? ToWithAddressesDto(Customer? c) {
        if(c != null) {
            var newCustomerWithAddressDto = new CustomerWithAddressesDto {
                Id = c.Id,
                Name = c.Name,
                Cpf = c.Cpf,
                Addresses = c.Addresses
                    .Select(a => AddressMapper.Instance.ToDto(a)).ToList()
            };

            return newCustomerWithAddressDto;
        }

        return null;
    }

    public CustomerPatchDto ToPatchDto(Customer c) {
        var newCustomerPatchDto = new CustomerPatchDto {
            Name = c.Name,
            Cpf = c.Cpf,
            Addresses = c.Addresses
                .Select(a => AddressMapper.Instance.ToDto(a)).ToList()
        };

        return newCustomerPatchDto;
    }

    public Customer ToCustomer(CustomerCreateDto dto) {
        return new Customer { Name = dto.Name, Cpf = dto.Cpf };
    }

    public Customer ToCustomer(CustomerWithAddressesCreateDto dto) {
        return new Customer { Name = dto.Name, Cpf = dto.Cpf };
    }

    public Customer ToCustomer(CustomerDto dto) {
        return new Customer { Id = dto.Id, Name = dto.Name, Cpf = dto.Cpf };
    }



    public static CustomerMapper Instance { 
        get { return _instance ??= new CustomerMapper(); } 
    }
}
