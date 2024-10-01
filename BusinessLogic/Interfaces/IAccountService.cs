using BusinessLogic.Models;

namespace BusinessLogic.Interfaces
{
    public interface IAccountService
    {
        Task<AuthResponse> GoogleLoginAsync(string googleAccessToken);
    }
}
