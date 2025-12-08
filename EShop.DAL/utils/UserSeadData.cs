using EShop.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EShop.DAL.utils
{
    public class UserSeadData : ISeedData
    {
        private readonly UserManager<AplecationUser> _UserManager;

        public UserSeadData(UserManager<AplecationUser> userManager)
        {
            _UserManager = userManager;
        }
        public async Task DAtaSeed()
        {
            if(!await _UserManager.Users.AnyAsync())
            {
                var user1 = new AplecationUser()
                {
                    UserName = "eAlawnah",
                    FullName = "emanAlawnah",
                    Email = "ALawnah.eman@gmail.com",
                    EmailConfirmed = true,
                };
                var user2 = new AplecationUser()
                {
                    UserName = "eAlawnah2",
                    FullName = "emanAlawnah2",
                    Email = "ALawnah.eman2@gmail.com",
                    EmailConfirmed = true,
                };
                var user3 = new AplecationUser()
                {
                    UserName = "eAlawnah3",
                    FullName = "emanAlawnah3",
                    Email = "ALawnah.eman3@gmail.com",
                    EmailConfirmed = true,
                };
                await _UserManager.CreateAsync(user1, "Eman@12345");
                await _UserManager.CreateAsync(user2, "Eman@123");
                await _UserManager.CreateAsync(user3, "Eman@1234");

                await _UserManager.AddToRoleAsync(user1, "SuperAdmin");
                await _UserManager.AddToRoleAsync(user2, "Admin");
                await _UserManager.AddToRoleAsync(user3, "User");
            }
        }
    }
}
