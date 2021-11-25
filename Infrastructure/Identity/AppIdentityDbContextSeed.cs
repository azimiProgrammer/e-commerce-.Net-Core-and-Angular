using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Admin",
                    Email = "Admin@admin.com",
                    UserName = "admin",
                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            FirstName = "amir",
                            LastName = "look",
                            MobileNumber = "09125770457",
                            Street="taleghani",
                            City="Tehran",
                            State="Tehran",
                            ZipCode="1435436789",
                            PostalAddress="pounak",
                            Lat=0,
                            Long=0,
                            IsActive=true
                        }
                    }
                };

                await userManager.CreateAsync(user, "P$$@sw0rd");
            }
        }
    }
}
