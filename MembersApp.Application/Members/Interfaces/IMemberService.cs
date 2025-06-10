using MembersApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersApp.Application.Members.Interfaces;
public interface IMemberService
{
    Task AddMemberAsync(Member member);
    Task<Member> GetMemberAsync(int Id);
    Task<List<Member>> GetAllMembersAsync();
    
}
