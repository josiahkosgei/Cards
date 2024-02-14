using Cards.Data.Enums;

namespace Cards.Core.Models
{
    public class CardDto : BaseResponse
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Color { get; set; }
        public CardStatus? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
