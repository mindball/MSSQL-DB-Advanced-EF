namespace ModelViewController
{
    using System.Collections.Generic;
    using System.Linq;

    using DataModel;
    using DataTransferObects;

    public class ArtistServiceWithoutMapper : IArtistsServiceExampleWithoutMapper
    {
        private readonly MusicXContext dbContext;

        public ArtistServiceWithoutMapper(MusicXContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<ArtistWithCountWithSmallProperties> 
            GetAllWithCountDTOWithSmallProperties()
        {
            //
            var songs = dbContext.Artists
                .Where(x => x.Id == 10)
                .Select(x => new ArtistWithCountWithSmallProperties
                {
                Name = x.Name,
                Count = x.SongArtists.Count()
                })
                .ToList();

            return songs;
        }

        public IEnumerable<ArtistWithCountWithBigProperties>
            GetAllWithCountDTOWithBigProperties()
        {
            var songs = dbContext.Artists
                .Where(x => x.Id == 10)
                .Select(x => new ArtistWithCountWithBigProperties
                {
                Name = x.Name,
                Count = x.SongArtists.Count(),
                CreatedOn = x.CreatedOn,
                ModifiedOn = x.ModifiedOn,
                IsDeleted = x.IsDeleted
                //and so on....
            })
            .ToList();

            return songs;
        }
    }
}
