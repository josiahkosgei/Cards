using Cards.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Cards.Data.Helpers.Migration
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context, ILogger<ApplicationDbContextSeed>? logger)
        {
            //Seed initial Data if no data in the table
            if (!context.Users.Any())
            {
                context.Users.AddRange(GetPreconfiguredUsers());
                await context.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}\n", nameof(ApplicationDbContext));
            }
            //Seed initial Data if no data in the table
            if (!context.Cards.Any())
            {
                context.Cards.AddRange(GetPreconfiguredCards());
                await context.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}\n", nameof(ApplicationDbContext));
            }
        }
        private static IList<User> GetPreconfiguredUsers()
        {
            var hasher = new PasswordHasher<User>();
            var passwordHash = hasher.HashPassword(null, "1234");
            // Fake Data
            var usersList = new List<User>()
            {
                new() {
                    Id = "9A2A49F5-4DE0-4283-A23E-9DE23A9278CD",
                    Name="Shirline Stuart",
                    Email="sstuart9@geocities.com",
                    NormalizedEmail ="sstuart9@geocities.com".ToUpper(),
                    PasswordHash=passwordHash,
                    Role=Enums.UserRole.Admin
                },
                 new() {
                    Id = "664E5B64-7F95-4268-B4F3-1678CF953150",
                    Name="Slade Dobbin",
                    Email="sdobbin4@weibo.com",
                    NormalizedEmail="sdobbin4@weibo.com".ToUpper(),
                    PasswordHash=passwordHash,
                    Role=Enums.UserRole.Member
                },
                  new() {
                    Id ="D0BB3FC0-1998-474C-970F-E64F8238BC05",
                    Name = "Aimee Sciusscietto",
                    Email = "asciusscietto5@mapy.cz",
                    NormalizedEmail = "asciusscietto5@mapy.cz".ToUpper(),
                    PasswordHash = passwordHash,
                    Role = Enums.UserRole.Member
                },
                  new() {
                    Id = "D6FCA9DD-EACE-49A4-A8EE-D9106A662A5B",
                    Name="Bobbie Jubert",
                    Email="bjubert7@cdbaby.com",
                    NormalizedEmail="bjubert7@cdbaby.com".ToUpper(),
                    PasswordHash=passwordHash,
                    Role=Enums.UserRole.Member
    }
};
            return usersList;
        }

        private static IList<Card> GetPreconfiguredCards()
        {
            // Fake Data in JSON format
            string cards = "[{\"Name\":\"Wynne\",\"Description\":\"Berthelmot\",\"Color\":\"#d7f6ce\",\"Status\":2,\"UserId\":\"D6FCA9DD-EACE-49A4-A8EE-D9106A662A5B\"},\r\n{\"Name\":\"Reine\",\"Description\":\"Blandamere\",\"Color\":\"#7c2521\",\"Status\":0,\"UserId\":\"D0BB3FC0-1998-474C-970F-E64F8238BC05\"},\r\n{\"Name\":\"Valene\",\"Description\":\"Langthorn\",\"Color\":\"#d3bcee\",\"Status\":2,\"UserId\":\"664E5B64-7F95-4268-B4F3-1678CF953150\"},\r\n{\"Name\":\"Willey\",\"Description\":\"Grindlay\",\"Color\":\"#355a71\",\"Status\":2,\"UserId\":\"D6FCA9DD-EACE-49A4-A8EE-D9106A662A5B\"},\r\n{\"Name\":\"Thatcher\",\"Description\":\"Humby\",\"Color\":\"#e272e7\",\"Status\":2,\"UserId\":\"D0BB3FC0-1998-474C-970F-E64F8238BC05\"},\r\n{\"Name\":\"Eamon\",\"Description\":\"Colebrook\",\"Color\":\"#225bc7\",\"Status\":1,\"UserId\":\"D6FCA9DD-EACE-49A4-A8EE-D9106A662A5B\"},\r\n{\"Name\":\"Kelly\",\"Description\":\"Blodgett\",\"Color\":\"#f56569\",\"Status\":1,\"UserId\":\"D6FCA9DD-EACE-49A4-A8EE-D9106A662A5B\"},\r\n{\"Name\":\"Laurel\",\"Description\":\"Busfield\",\"Color\":\"#83cd28\",\"Status\":2,\"UserId\":\"D6FCA9DD-EACE-49A4-A8EE-D9106A662A5B\"},\r\n{\"Name\":\"Colman\",\"Description\":\"Wilcinskis\",\"Color\":\"#5e8a5f\",\"Status\":0,\"UserId\":\"664E5B64-7F95-4268-B4F3-1678CF953150\"},\r\n{\"Name\":\"Ethelind\",\"Description\":\"Saurat\",\"Color\":\"#ae90a1\",\"Status\":1,\"UserId\":\"D6FCA9DD-EACE-49A4-A8EE-D9106A662A5B\"},\r\n{\"Name\":\"Debby\",\"Description\":\"Senecaux\",\"Color\":\"#a72458\",\"Status\":2,\"UserId\":\"D6FCA9DD-EACE-49A4-A8EE-D9106A662A5B\"},\r\n{\"Name\":\"Nick\",\"Description\":\"Sarre\",\"Color\":\"#03a055\",\"Status\":0,\"UserId\":\"D6FCA9DD-EACE-49A4-A8EE-D9106A662A5B\"},\r\n{\"Name\":\"Meggi\",\"Description\":\"Royste\",\"Color\":\"#b57f64\",\"Status\":2,\"UserId\":\"D0BB3FC0-1998-474C-970F-E64F8238BC05\"},\r\n{\"Name\":\"Harman\",\"Description\":\"Bendell\",\"Color\":\"#7b3971\",\"Status\":2,\"UserId\":\"D6FCA9DD-EACE-49A4-A8EE-D9106A662A5B\"},\r\n{\"Name\":\"Griff\",\"Description\":\"Kinsell\",\"Color\":\"#0dffeb\",\"Status\":2,\"UserId\":\"D6FCA9DD-EACE-49A4-A8EE-D9106A662A5B\"}]";
            var cardsList = JsonSerializer.Deserialize<List<Card>>(cards);
            return cardsList;
        }
    }
}
