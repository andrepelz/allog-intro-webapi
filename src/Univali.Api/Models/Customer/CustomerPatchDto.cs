namespace Univali.Api.Models;

/// <summary> DTO for Customer patching </summary>
public class CustomerPatchDto { 
    public string Name { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public List<AddressDto> Addresses { get; set; } = new List<AddressDto>();
}
