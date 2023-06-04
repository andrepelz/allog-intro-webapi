namespace Univali.Api.Models;

/// <summary> DTO for Address patching </summary>
public class AddressPatchDto { 
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}
