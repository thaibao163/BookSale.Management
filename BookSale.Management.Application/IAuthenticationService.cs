using BookSale.Management.Application.DTOs;

namespace BookSale.Management.Application
{
    public interface IAuthenticationService
    {
        Task<ResponseModel> CheckLogin(string username, string password, bool remember);
    }
}