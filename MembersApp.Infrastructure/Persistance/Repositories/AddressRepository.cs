using MembersApp.Application.Addresses.Interfaces;
using MembersApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersApp.Infrastructure.Persistance.Repositories;
public class AddressRepository(ApplicationContext context) : IAddressRepository
{
    public Task DeleteAddress(int Id)
    {
        throw new NotImplementedException();
    }

    public async Task<Address> GetAddress(int Id)
    {
        //return await context.Addresses.FindAsync(Id);
        throw new NotImplementedException();
    }

    public Task SetAddress(Address address)
    {
        throw new NotImplementedException();
    }
}
