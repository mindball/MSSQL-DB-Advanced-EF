using System.Collections.Generic;

namespace VaporStore.Services.Contracts
{
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
    }
}