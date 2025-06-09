using MembersApp.Application.Members.Interfaces;
using MembersApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersApp.Infrastructure.Persistance.Repositories;
public class MemberRepository(ApplicationContext context) : IMemberRepository
{
    public Task AddMemberAsync(Member member)
    {
        throw new NotImplementedException();
    }

    public Task DeleteMemberAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Member>> GetAllMembersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Member> GetMemberAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateMemberAsync(Member member)
    {
        throw new NotImplementedException();
    }
}
