namespace MembersApp.Web.Views.Member
{
    public class MembersVM
    {
        public MemberVM[] MembersVMs { get; set; } = null!;

        public class MemberVM
        {
            //public required int Id { get; set; }
            public required string Name { get; set; }
            public string? City { get; set; }
        }
    }
}
