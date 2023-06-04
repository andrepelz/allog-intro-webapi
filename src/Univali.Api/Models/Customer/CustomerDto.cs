namespace Univali.Api.Models;

/// <summary> DTO for Customer query </summary>
public class CustomerDto { 
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
}
