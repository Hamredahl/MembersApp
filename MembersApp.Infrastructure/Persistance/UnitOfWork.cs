using MembersApp.Application;
using MembersApp.Application.Addresses.Interfaces;
using MembersApp.Application.Members.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersApp.Infrastructure.Persistance
{
    public class UnitOfWork(
         ApplicationContext context,
         IMemberRepository member,
         IAddressRepository address) : IUnitOfWork
    {
        public IMemberRepository Members => member;
        public IAddressRepository Addresses => address;

        public async Task SaveAllAsync() => await context.SaveChangesAsync();
    }
}
