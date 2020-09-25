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

        public VaportService(VaporStoreContext context)
        {
            this.context = context;
        }

        public string Create(string name, 
                    decimal price, 
                    DateTime releaseDate, 
                    string developer, 
                    string genre, 
                    List<string> tags)
        {
            var game = new Game()
            {
                Name = name,
                Price = price,
                ReleaseDate = releaseDate
            };

            var genreEntity = this.context.Genres
                .FirstOrDefault(g => g.Name.Trim() == genre.Trim());

            if(genreEntity == null)
            {
                genreEntity = new Genre() { Name = genre };                
            }

            game.Genre = genreEntity;

            var developerEntity = this.context.Developers
                .FirstOrDefault(d => d.Name.Trim() == developer.Trim());

            if (developerEntity == null)
            {
                developerEntity = new Developer() { Name = developer };
            }

            game.Developer = developerEntity;

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
    }
}
