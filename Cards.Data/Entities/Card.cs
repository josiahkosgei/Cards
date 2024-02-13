using Cards.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Data.Entities
{
    public class Card: BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Color { get; set; }
        public CardStatus Status { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; } 
        public  User User { get; set; }
    }
}
