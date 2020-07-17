using System.Collections.Generic;

namespace FootballBettingDB.Data.Models
{
    public class Town
    {
        public Town()
        {
            this.Teams = new HashSet<Team>();
        }
        public int TownId { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }
        public Country Coutry { get; set; }


        public ICollection<Team> Teams { get; set; }
    }
}
