using MembersApp.Application;
using MembersApp.Application.Members.Interfaces;
using MembersApp.Application.Members.Services;
using MembersApp.Application.Users;
using MembersApp.Infrastructure.Persistance;
using MembersApp.Infrastructure.Persistance.Repositories;
using MembersApp.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MembersApp.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        builder.Services.AddScoped<IMemberRepository, MemberRepository>();
        builder.Services.AddScoped<IMemberService, MemberService>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        builder.Services.AddTransient<IUserService, UserService>();
        builder.Services.AddTransient<IIdentityUserService, IdentityUserService>();

        builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            //options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
        }).AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

        builder.Services.ConfigureApplicationCookie(o => o.LoginPath = "/login");

        var connString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationContext>(o => o.UseSqlServer(connString));

        var app = builder.Build();

        app.UseAuthorization();

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}
