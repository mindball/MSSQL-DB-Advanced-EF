////Controller or Services

namespace ModelViewController
{
    using System.Collections.Generic;
    using System.Linq;
   
    using DataModel;
    using DataTransferObects;

    public class ArtistsService : IArtistsService
    {
        private readonly MusicXContext dbContext;
        public ArtistsService(MusicXContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<DataTransferObects.ArtistWithCount> GetAllWithCount()
        {
            var songs = dbContext.Artists.Select(x => new ArtistWithCount
            {
                Name = x.Name,
                Count = x.SongArtists.Count()
            })
            .ToList();

            return songs;
        }
    }
}
