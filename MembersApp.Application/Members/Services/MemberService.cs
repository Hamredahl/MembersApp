using MembersApp.Application.Members.Interfaces;
using MembersApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersApp.Application.Members.Services;
public class MemberService(IUnitOfWork unitOfWork) : IMemberService
{
    public async Task AddMemberAsync(Member member)
    {
        if (member.Name != null)
        {
            string[] toCapitalize = member.Name.Split(' ');
            string capName = "";
            for (int i = 0; i < toCapitalize.Length; i++)
            {
                capName += ToInitalCapital(toCapitalize[i]);
                if (i != toCapitalize.Length - 1) capName += ' ';
            }
            member.Name = capName;
        }
        member.Email = member.Email?.ToLower();
        await unitOfWork.Members.AddMemberAsync(member);
        await unitOfWork.SaveAllAsync();

        string ToInitalCapital(string s) => $"{s[..1].ToUpper()}{s[1..]}";
    }

    public async Task<Member[]> GetAllMembersAsync()
    {
        var members = await unitOfWork.Members.GetAllMembersAsync();
        return [.. members.OrderBy(m => m.Name)];
    }

    public async Task<Member> GetMemberAsync(int Id)
    {
        Member? member = await unitOfWork.Members.GetMemberAsync(Id);
        return member is null ? throw new Exception("Member not found, invalid ID") : member;
    }
}
