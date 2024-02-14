using Cards.Data.Enums;
using Microsoft.AspNetCore.Identity;


namespace Cards.Data.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public UserRole Role { get; set; }
    }
}
