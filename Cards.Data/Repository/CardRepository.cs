using Cards.Data.Entities;
using Cards.Data.Enums;
using Cards.Data.Helpers;
using Cards.Data.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Cards.Data.Repository
{
    public class CardRepository : Repository<Card>, ICardRepository
    {
        private readonly IHttpContextAccessor _httpAccessor;
        public CardRepository(ApplicationDbContext dbContext, IHttpContextAccessor httpAccessor) : base(dbContext)
        {
            _httpAccessor = httpAccessor;
        }
        public async Task<List<Card>> GetAllAsync(string? name, string? color, CardStatus? status, DateTime? createdDate, string? sortBy, string? orderBy, int? page, int? size)
        {
            var currentUser = GetCurrentUser();
            var predicate = _context.Cards.AsQueryable();

            // apply filter by parameters
            if (!string.IsNullOrEmpty(name))
                predicate = predicate.Where(mc => mc.Name == name);

            if (status.HasValue)
                predicate = predicate.Where(mc => mc.Status == status.Value);

            if (createdDate.HasValue)
                predicate = predicate.Where(mc => mc.CreatedDate == createdDate.Value);

            if (!string.IsNullOrEmpty(color))
                predicate = predicate.Where(mc => mc.Color == color);

            if (currentUser.Role != "Admin")
                predicate = predicate.Where(mc => mc.UserId.ToUpper() == currentUser.Id.ToUpper());


            // apply sort by parameters
            switch (sortBy)
            {
                case "name":
                    predicate = orderBy == "desc" ? predicate.OrderByDescending(c => c.Name) : predicate.OrderBy(c => c.Name);
                    break;
                case "color":
                    predicate = orderBy == "desc" ? predicate.OrderByDescending(c => c.Color) : predicate.OrderBy(c => c.Color);
                    break;
                case "status":
                    predicate = orderBy == "desc" ? predicate.OrderByDescending(c => c.Status) : predicate.OrderBy(c => c.Status);
                    break;
                case "createdDate":
                    predicate = orderBy == "desc" ? predicate.OrderByDescending(c => c.CreatedDate) : predicate.OrderBy(c => c.CreatedDate);
                    break;
                default:
                    break;
            }

            // apply Pagination paramaters
            if (page.HasValue)
            {
                int limit = size.HasValue ? size.Value : 10;
                int offset = (page.Value - 1) * limit;
                predicate = predicate.Skip(offset).Take(limit);


            }
            var entities = await predicate.ToListAsync();
            return entities;
        }
        public new async Task<Card> GetAsync(Guid id)
        {
            var currentUser = GetCurrentUser();

            // Allow Admin to View the card even if they did not create it
            var entity = await _context.Set<Card>().FindAsync(id);
            if (entity != null && (entity.UserId.ToUpper() == currentUser.Id.ToUpper() || currentUser.Role == "Admin"))
            {
                return entity;
            }
            else
            {
                throw new UnauthorizedAccessException();
            }

        }
        public new async Task<Card> AddAsync(Card entity)
        {
            var currentUser = GetCurrentUser();
            entity.UserId = currentUser.Id;
            entity.Status = CardStatus.ToDo;
            _context.Set<Card>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
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
