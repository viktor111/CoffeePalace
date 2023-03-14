using CoffeePalace.Models.Common;
using CoffeePalace.Models.Types;

namespace CoffeePalace.Models.Entities;

public class User : Entity
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Address { get; set; }
    
    public string Country { get; set; }
    
    public string City { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public UserRoleType Role { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    public IEnumerable<Review> Reviews { get; set; }
}