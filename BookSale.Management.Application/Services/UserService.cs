using BookSale.Management.Application.DTOs;
using BookSale.Management.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookSale.Management.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ResponseModel> CheckLogin(string username, string password, bool remember)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user is null)
            {
                return new ResponseModel
                {
                    Message = "Username or password is invalid"
                };
            }

            var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: remember, lockoutOnFailure: true);

            if (result.IsLockedOut)
            {
                var remainingLockout = user.LockoutEnd.Value - DateTimeOffset.UtcNow;

                return new ResponseModel
                {
                    Message = $"Account is locked out. Please try again in {Math.Round(remainingLockout.TotalMinutes)} minutes",
                    RemainingSeconds = (int)remainingLockout.TotalSeconds
                };
            }

            if (!result.Succeeded)
            {
                return new ResponseModel
                {
                    Message = "Username or password is invalid"
                };
            }

            if (user.AccessFailedCount > 0)
            {
                await _userManager.ResetAccessFailedCountAsync(user);
            }

            return new ResponseModel
            {
                Status = true
            };
        }
    }
}
