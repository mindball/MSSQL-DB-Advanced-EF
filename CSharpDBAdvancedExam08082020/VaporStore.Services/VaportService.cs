using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using VaporStore.Data;
using VaporStore.Models;
using VaporStore.Models.Enums;
using VaporStore.Services.Contracts;

namespace VaporStore.Services
{
    public class VaportService : IVaportService
    {
        private VaporStoreContext context;
        private const string InvalidDataMsg = "Invalid Data";
        private string ValidDataMsg = "Added {gameName} ({gameGenre}) with {tagsCount} tags";

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

            var game = new Game()
            {
                Name = name,
                Price = price,
                ReleaseDate = DateTime.Parse(releaseDate)
            };

            game.Genre = GetOrCreateGenre(genre);

            game.Developer = GetOrCreateDeveloper(developer);

            foreach (var tag in tags)
            {
                var gameTag = new GameTags
                {
                    Tag = this.GetOrCreateTag(tag)
                };

                game.GamesTags.Add(gameTag);
            }

            this.context.Games.Add(game);
            this.context.SaveChanges();

            return $"Added {game.Name} ({game.Genre}) with {tags.Count} tags";
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

            var userEntity = this.context.Users
                .FirstOrDefault(u => u.Username.Trim() == username.Trim());

            if (userEntity == null)
            {
                userEntity = CreateUser(fullName, username, email, age);
                this.context.Users.Add(userEntity);
            }

            var card = GetOrCreateCard(number, cvc, type);

            userEntity.Cards.Add(card);            

            this.context.SaveChanges();

            return true;
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

        public string CreatePurchase()
        {
            return null;
        }

        private Tag GetOrCreateTag(string tagName)
        {
            var tagEntity = this.context.Tags
                .FirstOrDefault(t => t.Name.Trim() == tagName.Trim());

            if (tagEntity == null)
            {
                tagEntity = new Tag() { Name = tagName };
            }

            return tagEntity;
        }

        private Developer GetOrCreateDeveloper(string developer)
        {
            var developerEntity = this.context.Developers
                .FirstOrDefault(d => d.Name.Trim() == developer.Trim());

            if (developerEntity == null)
            {
                developerEntity = new Developer() { Name = developer };
            }

            return developerEntity;
        }

        private Genre GetOrCreateGenre(string genre)
        {
            var genreEntity = this.context.Genres
                .FirstOrDefault(d => d.Name.Trim() == genre.Trim());

            if (genreEntity == null)
            {
                genreEntity = new Genre() { Name = genre };
            }

            return genreEntity;
        }

        private Card GetOrCreateCard(string number, string cvc, string type)
        {
            var card = this.context.Cards
                .FirstOrDefault(c => c.Number == number);

            if (card == null)
            {
                card = new Card()
                {
                    Number = number,
                    Cvc = cvc,
                    Type = (CardType)Enum
                        .Parse(typeof(CardType), type)
                };
            }

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
    }
}
