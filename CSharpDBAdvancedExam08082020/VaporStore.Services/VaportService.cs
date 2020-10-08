namespace VaporStore.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore.Internal;
    using VaporStore.Data;
    using VaporStore.Models;
    using VaporStore.Models.Enums;
    using VaporStore.Services.Contracts;
    using VaporStore.Services.Models.ExportAllGamesByGenres;
    using VaporStore.Services.Models.ExportUserPurchasesByType;

    public class VaportService : IVaportService
    {
        private VaporStoreContext context;
        private const string InvalidDataMsg = "Invalid Data";

        private string ValidDataMsg = "Added {gameName} ({gameGenre}) with {tagsCount} tags";
        private string ValidPurchaseMsg = "Imported {0} for {1}";

        public VaportService(VaporStoreContext context)
        {
            this.context = context;
        }

        public string CreateGame(string name,
                    decimal price,
                    string releaseDate,
                    string developer,
                    string genre,
                    List<string> tags)
        {
            if (!CheckGameConstraint(name, price, releaseDate, developer, genre, tags))
            {
                return InvalidDataMsg;
            }

            var gameEntity = this.GetGame(name);

            if(gameEntity == null)
            {
                gameEntity = CreateGame(name, price, releaseDate);
            }           

            gameEntity.Genre = GetGenre(genre);

            if(gameEntity.Genre == null)
            {
                gameEntity.Genre = this.CreateGenre(genre);
            }

            gameEntity.Developer = GetDeveloper(developer);
            
            if(gameEntity.Developer == null)
            {
                gameEntity.Developer = this.CreateDeveloper(developer);
            }

            foreach (var tag in tags)
            {
                var gameTag = new GameTags
                {
                    Tag = this.GetTag(tag)                    
                };

                if(gameTag.Tag == null)
                {
                    gameTag.Tag = this.CreateTag(tag);
                }

                gameEntity.GamesTags.Add(gameTag);
            }

            this.context.Games.Add(gameEntity);
            this.context.SaveChanges();

            return $"Added {gameEntity.Name} ({gameEntity.Genre}) with {tags.Count} tags";
        }

        public bool IsValidCreateUserAndCard(string fullName,
            string username,
            string email,
            int age,
            bool existCards,
            string number,
            string cvc,
            string type
            )
        {
            if (!existCards)
            {
                return false;
            }

            if (!CheckUserConstraint(fullName, username, email, age))
            {
                return false;
            }

            if (!CheckCardConstraint(number, cvc, type))
            {
                return false;
            }

            var userEntity = this.GetUser(username);

            if (userEntity == null)
            {
                userEntity = CreateUser(fullName, username, email, age);
                this.context.Users.Add(userEntity);
            }

            var card = GetCard(number);

            if(card == null)
            {
                card = this.CreateCard(number, cvc, type);
            }

            userEntity.Cards.Add(card);            

            this.context.SaveChanges();

            return true;
        }

        public string CreatePurchase(string gameTitle, string purchaseType, 
            string productKey, string cardNumber, string dateTime)
        {
            //TODO: make constraint

            var card = GetCard(cardNumber);
            
            DateTime formatDateTime = DateTime.Parse(dateTime, CultureInfo.InvariantCulture);

            var purchaseEntity = 
                this.CreatePurchaseEntity(gameTitle, purchaseType, productKey, card.Number, formatDateTime);

            this.context.Purchases.Add(purchaseEntity);

            this.context.SaveChanges();

            return $"Imported {gameTitle} for {card.User.Username}";
        }

        
        private bool CheckCardConstraint(string number, string cvc, string type)
        {
            bool isValidConstraint = true;

            if (number == null || cvc == null || type == null)
            {
                isValidConstraint = false;
            }

            if (!Regex.IsMatch(number, @"^[0-9]{4}\s[0-9]{4}\s[0-9]{4}\s[0-9]{4}$"))
            {
                isValidConstraint = false;
            }

            if (!(int.TryParse(cvc, out _) && cvc.Length == 3))
            {
                isValidConstraint = false;
            }

            return isValidConstraint;
        }

        private bool CheckGameConstraint(string name,
                    decimal price,
                    string releaseDate,
                    string developer,
                    string genre, List<string> tags)
        {
            bool isValidConstraint = true;

            if (name == null || developer == null
                || genre == null || releaseDate == null)
            {
                isValidConstraint = false;
            }

            if (price < 0)
            {
                isValidConstraint = false;
            }

            if (tags is null || tags.Count <= 0)
            {
                isValidConstraint = false;
            }

            if (!IsvalidDateTime(releaseDate))
            {
                isValidConstraint = false;
            }

            return isValidConstraint;
        }

        private bool CheckUserConstraint(string fullName,
                string userName,
                string email,
                int age)
        {
            bool isValidUserConstraint = true;

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(fullName) ||
                string.IsNullOrEmpty(email) || age <= 0)
            {
                isValidUserConstraint = false;
                return isValidUserConstraint;
            }

            if (userName.Length < 3 || userName.Length > 20)
            {
                isValidUserConstraint = false;
                return isValidUserConstraint;
            }

            if (!Regex.IsMatch(fullName, @"^[A-Z][a-z]+\s[A-Z]+[a-z]+$"))
            {
                isValidUserConstraint = false;
                return isValidUserConstraint;
            }

            if (age < 3 || age > 103)
            {
                isValidUserConstraint = false;
                return isValidUserConstraint;
            }

            return isValidUserConstraint;
        }

        private bool IsvalidDateTime(string releaseDate)
        {
            DateTime validDate;

            if (DateTime.TryParse(releaseDate, out validDate))
            {
                String.Format("{0:d/MM/yyyy}", validDate);
                return true;
            }
            else
            {
                return false;
            }
        }

        private User GetUser(string username)
        {
            var userEntity = this.context.Users
               .FirstOrDefault(u => u.Username.Trim() == username.Trim());

            return userEntity;
        }

        private Tag GetTag(string tagName)
        {
            var tagEntity = this.context.Tags
                .FirstOrDefault(t => t.Name.Trim() == tagName.Trim());

            return tagEntity;
        }

        private Developer GetDeveloper(string developer)
        {
            var developerEntity = this.context.Developers
                .FirstOrDefault(d => d.Name.Trim() == developer.Trim());

            return developerEntity;
        }

        private Genre GetGenre(string genre)
        {
            var genreEntity = this.context.Genres
                .FirstOrDefault(d => d.Name.Trim() == genre.Trim());

            return genreEntity;
        }

        private Card GetCard(string number)
        {
            var card = this.context.Cards
                .FirstOrDefault(c => c.Number == number);

            return card;
        }

        private Game GetGame(string name)
        {
            var gameEntity = this.context.Games
                .FirstOrDefault(g => g.Name == name);

            return gameEntity;
        }

        //Create entities
        private Tag CreateTag(string tagName)
        {
            var tagEntity = new Tag() { Name = tagName };

            return tagEntity;
        }
        
        private Developer CreateDeveloper(string developer)
        {           
            var developerEntity  = new Developer() { Name = developer };            

            return developerEntity;
        }

        private Genre CreateGenre(string genre)
        {           
            var genreEntity = new Genre() { Name = genre };

            return genreEntity;
        }               

        private Card CreateCard(string number, string cvc, string type)
        {
           var card = new Card()
                {
                    Number = number,
                    Cvc = cvc,
                    Type = (CardType)Enum
                        .Parse(typeof(CardType), type)
                };

            return card;
        }

        private User CreateUser(string fullName, string username, string email, int age)
        {
            var userEntity = new User()
            {
                FullName = fullName,
                Username = username,
                Email = email,
                Age = age
            };
           
            return userEntity;
        }

        private Game CreateGame(string name, decimal price, string releaseDate)
        {
            var game = new Game()
            {
                Name = name,
                Price = price,
                ReleaseDate = DateTime.Parse(releaseDate)
            };

            return game;
        }

        private Purchase CreatePurchaseEntity(string gameTitle, string purchaseType, 
            string productKey, string cardNumber, DateTime dateTime)
        {
            var gameEntity = this.GetGame(gameTitle);
            var cardEntity = this.GetCard(cardNumber);

            var purchaseEntity = new Purchase()
            {
                Type = (PurchaseType)Enum
                        .Parse(typeof(PurchaseType), purchaseType),
                ProductKey = productKey,
                Date = dateTime,
                Game = gameEntity,
                Card = cardEntity
            };

            return purchaseEntity;
        }

        //public IEnumerable<GamesViewModel> SearchGames(IMapper Mapper)
        //{
        //    return this.context.Games
        //        .ProjectTo<GamesViewModel>(Mapper.ConfigurationProvider)
        //        .ToList();
        //}

        public IEnumerable<GenreViewModel> SearchGenres(IMapper Mapper)
        {
            return this.context.Genres
                .Where(g => g.Games.Any())
                .ProjectTo<GenreViewModel>(Mapper.ConfigurationProvider)
                .ToList(); 
        }

    }
}
