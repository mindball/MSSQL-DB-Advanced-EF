using System;

namespace ModelViewController.DataTransferObects
{
    public class ArtistWithCountWithBigProperties
    {
        public string Name { get; set; }

        public int Count { get; set; }

        //Added after example with more properties

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        //and so on.....

    }
}
