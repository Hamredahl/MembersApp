using MembersApp.Domain.Entities;

namespace MembersApp.Application.Addresses.Interfaces;
public interface IAddressRepository
{
    Task<Address> GetAddress(int Id);
    Task SetAddress(Address address);
    Task DeleteAddress(int Id);
}
