using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class Artist
    {
        public Artist()
        {
            ArtistMetadata = new HashSet<ArtistMetadata>();
            SongArtists = new HashSet<SongArtist>();
        }

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ArtistMetadata> ArtistMetadata { get; set; }
        public virtual ICollection<SongArtist> SongArtists { get; set; }
    }
}
