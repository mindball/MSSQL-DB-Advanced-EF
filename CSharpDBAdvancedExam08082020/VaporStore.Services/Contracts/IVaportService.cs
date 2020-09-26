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

        string CreateUserAndCard();

        string CreatePurchase();
    }
}
