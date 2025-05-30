using AutoMapper;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookSale.Management.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public UserService(UserManager<ApplicationUser> userManager,
                            SignInManager<ApplicationUser> signInManager,
                            RoleManager<IdentityRole> roleManager,
                            IImageService imageService,
                            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _imageService = imageService;
            _mapper = mapper;
        }

        public async Task<ResponseDataTable<UserModel>> GetUserByPagination(RequestDataTable requestDataTable)
        {
            var users = await _userManager.Users.Where(x => x.IsActive && (string.IsNullOrEmpty(requestDataTable.Keyword)
                                                 || (x.UserName.Contains(requestDataTable.Keyword)
                                                    || x.FullName.Contains(requestDataTable.Keyword)
                                                    || x.Email.Contains(requestDataTable.Keyword)
                                                    || x.PhoneNumber.Contains(requestDataTable.Keyword))))
                                            .Select(x => new UserModel
                                            {
                                                Email = x.Email,
                                                FullName = x.FullName,
                                                Phone = x.PhoneNumber,
                                                UserName = x.UserName,
                                                IsActive = x.IsActive ? "Yes" : "No",
                                                Id = x.Id
                                            }).ToListAsync();


            int totalRecords = users.Count;

            var result = users.Skip(requestDataTable.SkipItems).Take(requestDataTable.PageSize).ToList();

            return new ResponseDataTable<UserModel>
            {
                Draw = requestDataTable.Draw,
                RecordsTotal = totalRecords,
                RecordsFiltered = totalRecords,
                Data = result
            };
        }

        public async Task<AccountDto> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var role = (await _userManager.GetRolesAsync(user)).First();

            var userDto = _mapper.Map<AccountDto>(user);

            userDto.RoleName = role;

            return userDto;
        }

        public async Task<ResponseModel> Save(AccountDto account)
        {
            string errors = string.Empty;
            IdentityResult identityResult;

            if (string.IsNullOrEmpty(account.Id))
            {
                var applicationUser = new ApplicationUser
                {
                    FullName = account.Fullname,
                    UserName = account.Username,
                    IsActive = account.IsActive,
                    Email = account.Email,
                    MobilePhone = account.MobilePhone,
                    PhoneNumber = account.Phone,
                    Address = account.Address
                };

                identityResult = await _userManager.CreateAsync(applicationUser, account.Password);

                if (identityResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(applicationUser, account.RoleName);

                    await _imageService.SaveImage(new List<IFormFile> { account.Avatar }, "images/user", $"{applicationUser.Id}.png");

                    return new ResponseModel
                    {
                        Action = Domain.Enums.ActionType.Insert,
                        Message = "Insert successful",
                        Status = true,
                    };
                }
            }
            else
            {
                //update
                var user = await _userManager.FindByIdAsync(account.Id);

                user.FullName = account.Fullname;
                user.IsActive = account.IsActive;
                user.Email = account.Email;
                user.MobilePhone = account.MobilePhone;
                user.PhoneNumber = account.Phone;
                user.Address = account.Address;

                identityResult = await _userManager.UpdateAsync(user);

                //check role
                if (identityResult.Succeeded)
                {
                    await _imageService.SaveImage(new List<IFormFile> { account.Avatar }, "images/user", $"{user.Id}.png");

                    var hasRole = await _userManager.IsInRoleAsync(user, account.RoleName);

                    if (!hasRole)
                    {
                        var roleName = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

                        var removeResult = await _userManager.RemoveFromRoleAsync(user, roleName);

                        if (removeResult.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, account.RoleName);
                        }
                    }

                    return new ResponseModel
                    {
                        Status = true,
                        Message = "Update successful",
                        Action = Domain.Enums.ActionType.Update
                    };
                }
            }

            errors = string.Join("<br />", identityResult.Errors.Select(x => x.Description));

            return new ResponseModel
            {
                Action = Domain.Enums.ActionType.Insert,
                Message = $"{(string.IsNullOrEmpty(account.Id) ? "Insert" : "Update")} failes, {errors}",
                Status = false,
            };
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is not null)
            {
                user.IsActive = false;
                await _userManager.UpdateAsync(user);

                return true;
            }

            return false;
        }
    }
}
