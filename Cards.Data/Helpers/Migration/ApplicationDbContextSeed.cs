using Cards.Data.Entities;
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
            string users = "[\r\n{\"Name\":\"Shirline Stuart\",\"Email\":\"sstuart9@geocities.com\",\"Role\":0},\r\n{\"Name\":\"Yvor Copes\",\"Email\":\"ycopes0@ezinearticles.com\",\"Role\":0},\r\n{\"Name\":\"Kerwinn Faltskog\",\"Email\":\"kfaltskog1@gizmodo.com\",\"Role\":1},\r\n{\"Name\":\"Brennen De Carteret\",\"Email\":\"bde2@drupal.org\",\"Role\":1},\r\n{\"Name\":\"Rowney Woolway\",\"Email\":\"rwoolway3@nifty.com\",\"Role\":1},\r\n{\"Name\":\"Slade Dobbin\",\"Email\":\"sdobbin4@weibo.com\",\"Role\":1},\r\n{\"Name\":\"Aimee Sciusscietto\",\"Email\":\"asciusscietto5@mapy.cz\",\"Role\":1},\r\n{\"Name\":\"Gibb Wressell\",\"Email\":\"gwressell6@weather.com\",\"Role\":1},\r\n{\"Name\":\"Bobbie Jubert\",\"Email\":\"bjubert7@cdbaby.com\",\"Role\":1},\r\n{\"Name\":\"Gardie Annwyl\",\"Email\":\"gannwyl8@a8.net\",\"Role\":1}]";
            var usersList = JsonSerializer.Deserialize<List<User>>(users);
            return usersList;
        }

        private static IList<Card> GetPreconfiguredCards()
        {
            string cards = "[{\"Name\":\"Wynne\",\"Description\":\"Berthelmot\",\"Color\":\"#d7f6ce\",\"Status\":2,\"UserId\":1},\r\n{\"Name\":\"Reine\",\"Description\":\"Blandamere\",\"Color\":\"#7c2521\",\"Status\":0,\"UserId\":3},\r\n{\"Name\":\"Valene\",\"Description\":\"Langthorn\",\"Color\":\"#d3bcee\",\"Status\":2,\"UserId\":2},\r\n{\"Name\":\"Willey\",\"Description\":\"Grindlay\",\"Color\":\"#355a71\",\"Status\":2,\"UserId\":1},\r\n{\"Name\":\"Thatcher\",\"Description\":\"Humby\",\"Color\":\"#e272e7\",\"Status\":2,\"UserId\":3},\r\n{\"Name\":\"Eamon\",\"Description\":\"Colebrook\",\"Color\":\"#225bc7\",\"Status\":1,\"UserId\":1},\r\n{\"Name\":\"Kelly\",\"Description\":\"Blodgett\",\"Color\":\"#f56569\",\"Status\":1,\"UserId\":1},\r\n{\"Name\":\"Laurel\",\"Description\":\"Busfield\",\"Color\":\"#83cd28\",\"Status\":2,\"UserId\":1},\r\n{\"Name\":\"Colman\",\"Description\":\"Wilcinskis\",\"Color\":\"#5e8a5f\",\"Status\":0,\"UserId\":2},\r\n{\"Name\":\"Ethelind\",\"Description\":\"Saurat\",\"Color\":\"#ae90a1\",\"Status\":1,\"UserId\":1},\r\n{\"Name\":\"Debby\",\"Description\":\"Senecaux\",\"Color\":\"#a72458\",\"Status\":2,\"UserId\":1},\r\n{\"Name\":\"Nick\",\"Description\":\"Sarre\",\"Color\":\"#03a055\",\"Status\":0,\"UserId\":1},\r\n{\"Name\":\"Meggi\",\"Description\":\"Royste\",\"Color\":\"#b57f64\",\"Status\":2,\"UserId\":3},\r\n{\"Name\":\"Harman\",\"Description\":\"Bendell\",\"Color\":\"#7b3971\",\"Status\":2,\"UserId\":1},\r\n{\"Name\":\"Griff\",\"Description\":\"Kinsell\",\"Color\":\"#0dffeb\",\"Status\":2,\"UserId\":1}]";
            var cardsList = JsonSerializer.Deserialize<List<Card>>(cards);
            return cardsList;
        }
    }
}
