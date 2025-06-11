using MembersApp.Application.Members.Interfaces;
using MembersApp.Domain.Entities;

namespace MembersApp.Application.Members.Services;
public class MemberService(IUnitOfWork unitOfWork) : IMemberService
{
    public async Task AddMemberAsync(Member member)
    {
        NormalizeMemberName(member);
        member.Email = member.Email?.ToLower();

        await unitOfWork.Members.AddMemberAsync(member);
        await unitOfWork.SaveAllAsync();
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

    private static void NormalizeMemberName(Member member)
    {
        if (member.Name != null)
        {
            string[] toCapitalize = member.Name.ToLowerInvariant().Split(' ');
            string capName = "";
            for (int i = 0; i < toCapitalize.Length; i++)
            {
                capName += ToInitialCapital(toCapitalize[i]);
                if (i != toCapitalize.Length - 1) capName += ' ';
            }
            member.Name = capName;
        }
        string ToInitialCapital(string s) => $"{s[..1].ToUpper()}{s[1..]}";
    }
}
