
namespace VaporStore.Services.Contracts
{
    using AutoMapper;

    using System.Collections.Generic;
    using VaporStore.Services.Models.ExportAllGamesByGenres;
    using VaporStore.Services.Models.ExportUserPurchasesByType;

    public interface IVaportService
    {
        string CreateGame(string name,
            decimal price,
            string releaseDate,
            string developer,
            string genre,
            List<string> tags);

        bool IsValidCreateUserAndCard(string fullName, string username, string email, int age,
            bool existCards, string number, string cvc, string type);

        string CreatePurchase(string gameTitle, string purchaseType, string productKey, string cardNumber, string dateTime);

        //IEnumerable<GamesViewModel> SearchGames(IMapper Mapper);

        IEnumerable<GenreViewModel> SearchGenres(IMapper Mapper);
      
    }
}