using Cards.Core.Models;
using Cards.Data.Enums;

namespace Cards.Core.Services.Interfaces
{
    public interface ICardService
    {
        Task<IList<CardDto>> GetAllAsync(string? name, string? color, CardStatus? status, DateTime? createdDate, string? sortBy, string? orderBy, int? page, int? size);
        Task<CardDto> GetAsync(Guid id);
        Task<CardDto> AddAsync(CreateCardDto cardDto);
        Task<CardDto> UpdateAsync(Guid id, UpdateCardDto cardDto);
        Task<CardDto> DeleteAsync(Guid id);
    }
}
