using Microsoft.AspNetCore.Mvc;
using Univali.Api.Entities;

namespace Univali.Api.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase {
    private CustomerMapper _mapper = CustomerMapper.Instance;

    

    [HttpGet]
    public ActionResult<IEnumerable<CustomerDto> > GetCustomers() {
        var result = new List<CustomerDto>();

        foreach(var c in Data.Instance.Customers)
            result.Add(_mapper.ToDto(c));

        return Ok(result);
    }

    [HttpGet("{id}", Name = "GetCustomerById")]
    public ActionResult<CustomerDto> GetCustomerById(int id) {
        var customer = Data.Instance.Customers.FirstOrDefault(x => x.Id == id);
        var result = _mapper.ToDto(customer);

        return result != null ? Ok(result) : NotFound();
    }

    [HttpGet("cpf/{cpf}", Name = "GetCustomerByCpf")]
    public ActionResult<CustomerDto> GetCustomerByCpf(string cpf) {
        var customer = Data.Instance.Customers.FirstOrDefault(x => x.Cpf == cpf);
        var result = _mapper.ToDto(customer);

        return result != null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public ActionResult<CustomerCreateDto> CreateCustomer(CustomerCreateDto customer) {
        int id = Data.Instance.Customers.Max(c => c.Id) + 1;
        var newCustomer = _mapper.ToCustomer(id, customer);

        Data.Instance.Customers.Add(newCustomer);

        return CreatedAtRoute(
            "GetCustomerById",
            new { id = newCustomer.Id },
            customer
        );
    }

    [HttpPut("{id}")]
    public ActionResult<CustomerCreateDto> UpdateCustomer(int id, CustomerCreateDto customer) {
        var newCustomer = Data.Instance.Customers.FirstOrDefault(c => c.Id == id);

        if(newCustomer != null) {
            newCustomer.Name = customer.Name;
            newCustomer.Cpf = customer.Cpf;

            return Ok(customer);
        } else {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCustomer(int id) {
        var customers = Data.Instance.Customers;
        customers.Remove(customers.FirstOrDefault(c => c.Id == id));
        return NoContent();
    }
}
