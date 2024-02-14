using Cards.Core.Models;
using Cards.Data.Enums;

namespace Cards.Core.Services.Interfaces
{
    public interface ICardService
    {
        Task<IList<CardDto>> GetAllAsync(string? name, string? color, CardStatus? status, DateTime? createdDate);
        Task<CardDto> GetAsync(Guid id);
        Task<CardDto> AddAsync(CardDto cardDto);
        Task<CardDto> UpdateAsync(Guid id, CardDto cardDto);
        Task<bool> DeleteAsync(CardDto cardDto);
    }
}
