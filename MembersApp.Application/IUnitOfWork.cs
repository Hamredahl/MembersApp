using MembersApp.Application.Addresses.Interfaces;
using MembersApp.Application.Members.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersApp.Application;
public interface IUnitOfWork
{
    IMemberRepository Members { get; }
    IAddressRepository Adresses { get; }
    Task SaveAllAsync();
}
