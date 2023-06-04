namespace Univali.Api.Models;

/// <summary> DTO for Address query </summary>
public class AddressDto { 
    public int Id { get; set; }
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}