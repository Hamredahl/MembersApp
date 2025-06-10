using MembersApp.Application;
using MembersApp.Application.Addresses.Interfaces;
using MembersApp.Application.Members.Interfaces;
using MembersApp.Application.Members.Services;
using MembersApp.Infrastructure.Persistance;
using MembersApp.Infrastructure.Persistance.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MembersApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddTransient<IMemberRepository, MemberRepository>();
            builder.Services.AddTransient<IAddressRepository, AddressRepository>();

            builder.Services.AddTransient<IMemberService, MemberService>();
            //builder.Services.AddScoped<ICompanyService, CompanyService>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Identity: Registera identity-klasserna och vilken DbContext som ska användas
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                // Här kan vi (om vi vill) ange inställningar för t.ex. lösenord
                // (ofta struntar man i detta och kör på default-värdena)
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
            }).AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

            // Identity: Hit ska icke inloggade användare skikas (om de besöker skyddade sidor)
            builder.Services.ConfigureApplicationCookie(o => o.LoginPath = "/login");

            // Hämta connection-strängen från AppSettings.json​
            var connString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Registrera Context-klassen för dependency injection​
            builder.Services.AddDbContext<ApplicationContext>(o => o.UseSqlServer(connString));

            var app = builder.Build();

            app.MapControllers();
            app.Run();
        }
    }
}
