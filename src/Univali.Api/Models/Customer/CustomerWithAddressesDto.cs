namespace Univali.Api.Models;

/// <summary> DTO for Customer query alongside related addresses </summary>
public class CustomerWithAddressesDto { 
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public List<AddressDto> Addresses { get; set; } = new List<AddressDto>();
}
