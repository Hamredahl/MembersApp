using MembersApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersApp.Application.Addresses.Interfaces;
public interface IAddressRepository
{
    Task<Address> GetAddress(int Id);
    Task SetAddress(Address address);
    Task DeleteAddress(int Id);
}
