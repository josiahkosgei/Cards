using Cards.Data.Entities;
using Cards.Data.Enums;
using Cards.Data.Helpers;
using Cards.Data.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Cards.Data.Repository
{
    public class CardRepository : Repository<Card>, ICardRepository
    {
        private readonly IHttpContextAccessor _httpAccessor;
        public CardRepository(ApplicationDbContext dbContext, IHttpContextAccessor httpAccessor) : base(dbContext)
        {
            _httpAccessor = httpAccessor;
        }
        public async Task<List<Card>> GetAllAsync(string? name, string? color, CardStatus? status, DateTime? createdDate)
        {
            var currentUser = GetCurrentUser();
            var predicate = _context.Cards.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                predicate = predicate.Where(mc => mc.Name == name);
            }
            if (status.HasValue)
            {
                predicate = predicate.Where(mc => mc.Status == status.Value);
            }

            if (createdDate.HasValue)
            {
                predicate = predicate.Where(mc => mc.CreatedDate == createdDate.Value);
            }
            if (!string.IsNullOrEmpty(color))
            {

                predicate = predicate.Where(mc => mc.Color == color);
            }
            if (currentUser.Role != "Admin")
            {
                predicate = predicate.Where(mc => mc.UserId.ToUpper() == currentUser.Id.ToUpper());
            }
            var entities = await predicate.ToListAsync();
            return entities;
        }

        // Get Current User using  Http Accessor class
        private CurrentUserModel GetCurrentUser()
        {
            var identity = _httpAccessor.HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var claims = identity.Claims;
                return new CurrentUserModel
                {
                    Email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                    Id = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                    Role = claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }
    }
}
