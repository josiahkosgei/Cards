using Cards.Core.Models;

namespace Cards.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
