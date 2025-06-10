using MembersApp.Application.Members.Interfaces;
using MembersApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersApp.Infrastructure.Persistance.Repositories;
public class MemberRepository(ApplicationContext context) : IMemberRepository
{
    public async Task AddMemberAsync(Member member)
    {
        await context.Members.AddAsync(member);
    }

    public Task DeleteMemberAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Member[]> GetAllMembersAsync() =>
        await context.Members.AsNoTracking().Include(a => a.MemberAddress).ToArrayAsync();

    public async Task<Member?> GetMemberAsync(int id) => await context.Members.FindAsync(id);

    public Task UpdateMemberAsync(Member member)
    {
        throw new NotImplementedException();
    }
}
