using MembersApp.Application;
using MembersApp.Application.Members.Interfaces;
using MembersApp.Application.Members.Services;
using MembersApp.Application.Users;
using MembersApp.Infrastructure.Persistance;
using MembersApp.Infrastructure.Persistance.Repositories;
using MembersApp.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MembersApp.Web;

public class Program
{
    public static async Task Main(string[] args)
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


        //Seed the database with an admin user in development mode
            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IUserService>();
                await service.CreateUserAsync("Olena", "1234Aa", isAdmin : true);
            }

        app.UseAuthorization();

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}
