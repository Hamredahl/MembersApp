using MembersApp.Application;
using MembersApp.Application.Addresses.Interfaces;
using MembersApp.Application.Members.Interfaces;
using MembersApp.Application.Members.Services;
using MembersApp.Infrastructure.Persistance;
using MembersApp.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MembersApp.Terminal;

public class Program
{
    private static IUnitOfWork unitOfWork;
    static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var services = new ServiceCollection();
        services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IMemberService, MemberService>();

        using var serviceProvider = services.BuildServiceProvider();

        unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        ListAllMembers();
    }

    private static void ListAllMembers()
    {
        var members = unitOfWork.Members.GetAllMembersAsync().Result;
        foreach (var member in members)
        {
            Console.WriteLine($"Name: {member.Name}");
        }
    }
}
