using Cards.Data.Entities;
using Cards.Data.Enums;

namespace Cards.Data.IRepository
{
    public interface ICardRepository : IRepository<Card>
    {
        Task<List<Card>> GetAllAsync(string? name, string? color, CardStatus? status, DateTime? createdDate, string? sortBy, string? orderBy, int? page, int? size);
    }
}
