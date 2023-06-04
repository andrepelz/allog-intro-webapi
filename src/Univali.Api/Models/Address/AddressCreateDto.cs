namespace Univali.Api.Models;

/// <summary> DTO for Address creation </summary>
public class AddressCreateDto { 
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}
