namespace Univali.Api.Models;

/// <summary> DTO for Customer creation </summary>
public class CustomerWithAddressesCreateDto { 
    public string Name { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public List<AddressCreateDto> Addresses { get; set; } = new List<AddressCreateDto>();
}
