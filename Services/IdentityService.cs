using aspnetcore_vue.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetcore_vue.Services
{
    public class IdentityService
    {

        public UserManager<AppUser> UserManager { get; }
        public SignInManager<AppUser> SignInManager { get; }
        private RoleManager<AppRole> RoleManager;

        public IdentityService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        public async Task<SignInResult> SignIn(string username, string password)
        {
            return await SignInManager.PasswordSignInAsync(username, password, false, false);
        }

        public async Task SignOut()
        {
            await SignInManager.SignOutAsync();
        }

        public async Task<bool> Register(UserRegisterModel model)
        {
            var user = await GetUser(model.Username);
            
            if (user == null)
            {
                user = new AppUser
                {
                    UserName = model.Username,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                return (await UserManager.CreateAsync(user, model.Password)).Succeeded;
            }
            return false;
        }

        private async Task<AppUser> GetUser(string username)
        {
            return await UserManager.FindByNameAsync(username);
        }

        public async Task<UserProfileModel> GetUserProfile(string username)
        {
            var user = await GetUser(username);

            return new UserProfileModel
            {
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }



    }
}
