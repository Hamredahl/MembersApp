using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersApp.Domain.Entities;
public class Address
{
    public int Id { get; set; }
    public string? Street { get; set; }
    public string? ZipNumber { get; set; }
    public string? City { get; set; }
}
