namespace Univali.Api.Models;

/// <summary> DTO for Customer creation </summary>
public class CustomerCreateDto { 
    public string Name { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
}
