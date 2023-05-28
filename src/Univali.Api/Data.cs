using Univali.Api.Entities;

namespace Univali.Api;

public class Data {
    private static Data? _instance;

    private Data() { 
        Customers = new List<Customer>() {
            new Customer {
                Id = 1,
                Name = "Linus Torvalds",
                Cpf = "12345678901"
            },
            new Customer {
                Id = 2, 
                Name = "Bill Gates", 
                Cpf = "10987654321"
            }
        }; 
    }

    public static Data Instance { 
        get { return _instance ??= new Data(); } 
    }
    public List<Customer> Customers { get; set; }
}