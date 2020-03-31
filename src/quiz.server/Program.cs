using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace quiz.server
{
    public class Program
    {

        public static void Main(string[] args)
        {
            //BuildWebHost(args).Run();
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            var host = BuildWebHost(args).Build();

            const string name = "admin@example.com";
            const string password = "Quiz@dmin2020";
            const string roleId = "Admin";
        
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                IdentityResult roleResult;
                var role = await roleManager.FindByNameAsync(roleId);
                //Create Role Admin if it does not exist
                if(role == null){
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleId));
                }

                var user = await userManager.FindByNameAsync(name);
                if (user == null) {
                    user = new IdentityUser { UserName = name, Email = name };
                    var result = await userManager.CreateAsync(user, password);
                }

                // Add user admin to Role Admin if not already added
                var rolesForUser = await userManager.GetRolesAsync(user);
                if (!rolesForUser.Contains(roleId)) {
                    var result = await userManager.AddToRoleAsync(user, roleId);
                }
                
            }

            await host.RunAsync();        
        }

        public static IHostBuilder BuildWebHost(string[] args) =>
                Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
