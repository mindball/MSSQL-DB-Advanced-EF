using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using VaporStore.Data;
using VaporStore.Models;
using VaporStore.Services.Contracts;

namespace VaporStore.Services
{
    public class VaportService : IVaportService
    {
        private VaporStoreContext context;
        private const string InvalidDataMsg = "Invalid Data";
        private  string ValidDataMsg = "Added {gameName} ({gameGenre}) with {tagsCount} tags";

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
            if(!CheckConstraint(name, price, releaseDate, developer, genre, tags))
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

        private bool CheckConstraint(string name,
                    decimal price,
                    string releaseDate,
                    string developer,
                    string genre, List<string> tags)
        {
            bool isValidConstraint = true;

            if(name == null || developer == null 
                || genre == null || releaseDate == null)
            {
                isValidConstraint = false;
            }

            if(price < 0)
            {
                isValidConstraint = false;
            }

            if(tags is null || tags.Count <= 0)
            {
                isValidConstraint = false;
            }

            if(!IsvalidDateTime(releaseDate))
                isValidConstraint = false;

            return isValidConstraint;
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
       
        public string CreateUserAndCard()
        {
            throw new NotImplementedException();
        }

        public string CreatePurchase()
        {
            return null;
        }

        private Tag GetOrCreateTag(string tagName)
        {
            var tagEntity = this.context.Tags
                .FirstOrDefault(t => t.Name.Trim() == tagName.Trim());

            if(tagEntity == null)
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
               
    }
}
