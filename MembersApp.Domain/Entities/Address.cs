using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersApp.Domain.Entities;
public class Address
{
    public int Id { get; set; }
    public string? Street { get; set; } //Never shown in runtime, used in test, remove?
    public string? ZipNumber { get; set; } //Never shown in runtime, used in test, remove?
    public string? City { get; set; }
}
