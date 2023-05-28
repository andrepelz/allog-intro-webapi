namespace Univali.Api.Entities;

public class Customer {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
}



/// <summary> DTO for Customer query </summary>
public class CustomerDto { 
    public string Name { get; set; } = string.Empty;
}

/// <summary> DTO for Customer creation </summary>
public class CustomerCreateDto { 
    public string Name { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
}


/// <summary> Singleton DTO mapper for Customer </summary>
public class CustomerMapper { 
    private static CustomerMapper? _instance;

    private CustomerMapper() {}



    public CustomerDto? ToDto(Customer? c) {
        return c != null ? new CustomerDto { Name = c.Name } : null;
    }

    public Customer ToCustomer(int id, CustomerCreateDto dto) {
        return new Customer { Id = id, Name = dto.Name, Cpf = dto.Cpf };
    }



    public static CustomerMapper Instance { 
        get { return _instance ??= new CustomerMapper(); } 
    }
}
