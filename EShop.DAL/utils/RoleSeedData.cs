using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.utils
{
    public class RoleSeedData : ISeedData
    {
        private readonly RoleManager<IdentityRole> _RoleManager;

        public RoleSeedData(RoleManager<IdentityRole> roleManager)
        {
            _RoleManager = roleManager;
        }

        string[] Roles = ["Admin", "User", "SuperAdmin"];
        public async Task DAtaSeed()
        {
            if (!await _RoleManager.Roles.AnyAsync())
            {
                foreach (var role in Roles)
                {
                    await _RoleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
