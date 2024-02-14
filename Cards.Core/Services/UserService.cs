using Cards.Core.Enums;
using Cards.Core.Models;
using Cards.Core.Services.Interfaces;
using Cards.Data;
using Cards.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Cards.Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        public UserService(UserManager<User> userManager, ApplicationDbContext context, IConfiguration config)
        {
            _userManager = userManager;
            _context = context;
            _config = config;
        }
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {

            var managedUser = await _userManager.FindByEmailAsync(request.Email!);
            if (managedUser == null)
            {
                return new LoginResponse { Message = ResponseEnum.InvalidCredentials.EnumFormat().desc };
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password!);
            if (!isPasswordValid)
            {
                return new LoginResponse { Message = ResponseEnum.InvalidCredentials.EnumFormat().desc, };
            }
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            var accessToken = JWTService.GenerateJWT(user!, _config["JWT:SecretKey"]!);
            return new LoginResponse { Message = ResponseEnum.Successful.EnumFormat().desc, AccessToken = accessToken };

        }
    }
}
