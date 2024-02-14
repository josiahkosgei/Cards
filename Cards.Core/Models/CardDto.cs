using Cards.Data.Enums;

namespace Cards.Core.Models
{
    public class CardDto : BaseResponse
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Color { get; set; }
        public CardStatus Status { get; set; }
        public Guid UserId { get; set; }
    }
}
