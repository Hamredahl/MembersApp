using MembersApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersApp.Application.Members.Interfaces;
public interface IMemberRepository
{
    Task<Member> GetMemberAsync(int id);
    Task<List<Member>> GetAllMembersAsync();
    Task AddMemberAsync(Member member);
    Task DeleteMemberAsync(int id);
    Task UpdateMemberAsync(Member member);
}
