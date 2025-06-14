﻿using BookSale.Management.Application.DTOs;

namespace BookSale.Management.Application
{
    public interface IUserService
    {
        Task<bool> DeleteAsync(string id);
        Task<AccountDto> GetUserById(string id);
        Task<ResponseDataTable<UserModel>> GetUserByPagination(RequestDataTable requestDataTable);
        Task<ResponseModel> Save(AccountDto account);
    }
}