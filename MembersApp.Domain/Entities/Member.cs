using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersApp.Domain.Entities;
public class Member
{
    public int Id { get; set; }
    public string? Name { get; set; } 
    public string? Email { get; set; } //Never shown in runtime, used in test, remove?
    public string? Phone { get; set; } //Never shown in runtime, used in test, remove?
    public int? AddressId { get; set; }
    public Address? MemberAddress { get; set; }
}
