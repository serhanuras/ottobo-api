using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ottobo.Entities;
using Ottobo.Infrastructure.Data.PostgreSql;
using Ottobo.Infrastructure.Extensions;
using Ottobo.Services;

namespace Ottobo.Api.Middlewares
{
    public class StartUpMiddleware
    {
        
       
        private readonly UserService _userService;
        private readonly RoleService _roleService;

        public StartUpMiddleware(UserService userService, RoleService roleService)
        {
            
            _userService = userService;
            _roleService = roleService;
        }

        public void InitStartUp()
        {
            AddRoles();
        }


        private async void AddRoles()
        {
            var roles = _roleService.Read(1, 100);
            

           if (!roles.Exists(e => e.Name == "Super User"))
           {
               _roleService.Create(new Role()
               {
                   Name = "Super User"
               });
           }
           
           if (!roles.Exists(e => e.Name == "Admin"))
           {
               _roleService.Create(new Role()
               {
                   Name = "Admin"
               });
           }
           
           if (!roles.Exists(e => e.Name == "Guest User"))
           {
               _roleService.Create(new Role()
               {
                   Name = "Guest User"
               });
           }


           var superUser = _userService.Read().Where(e => e.Email == "super@ottobo.com");


           var superUserRole = _roleService.Filter("", DataSortType.Asc, 1, 100, "Super User")[0];
           

           if (!superUser.Any())
           {
               var user = new User
               {
                   
                   Email = "super@ottobo.com",
                   FirstName =  "Ottobo",
                   LastName = "Super Admin",
                   RoleId = superUserRole.Id,
                   Password = "SDFwer741".HashPassword("super@ottobo.com")
               };

               _userService.Create(user);
           }

           
        }
        
    }
}