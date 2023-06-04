namespace Univali.Api.Models;

/// <summary> DTO for Address updating </summary>
public class AddressUpdateDto { 
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}
