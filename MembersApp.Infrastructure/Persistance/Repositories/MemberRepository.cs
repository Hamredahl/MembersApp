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

    public async Task<Member[]> GetAllMembersAsync() =>
        await context.Members.AsNoTracking().Include(a => a.MemberAddress).ToArrayAsync();

}
