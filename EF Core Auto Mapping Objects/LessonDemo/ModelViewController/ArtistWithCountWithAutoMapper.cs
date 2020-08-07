using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataModel;
using DataModel.Models;
using ModelViewController.DataTransferObects;
using System.Collections.Generic;
using System.Linq;

namespace ModelViewController
{
    public class ArtistWithCountWithAutoMapper : IArtistsServiceExampleWithMapper
    {

        private readonly MusicXContext dbContext;

        public ArtistWithCountWithAutoMapper(MusicXContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IMapper mapper { get; set; }       

        public IEnumerable<ArtistWithCountWithBigProperties> GetAllWithCountDTOWithBigProperties()
        {
            var config = new MapperConfiguration(
               cfg => cfg.CreateMap<Artist, ArtistWithCountWithBigProperties>()); ;

            this.mapper = config.CreateMapper();

            var artist = this.dbContext.Artists                
                .ProjectTo<ArtistWithCountWithBigProperties>(config)
               .ToList();

            return artist;
        }
    }
}
