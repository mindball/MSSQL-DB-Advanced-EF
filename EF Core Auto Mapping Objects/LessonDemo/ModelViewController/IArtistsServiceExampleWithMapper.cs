using ModelViewController.DataTransferObects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelViewController
{
    public interface IArtistsServiceExampleWithMapper
    {
        IEnumerable<ArtistWithCountWithBigProperties> GetAllWithCountDTOWithBigProperties();
    }
}
