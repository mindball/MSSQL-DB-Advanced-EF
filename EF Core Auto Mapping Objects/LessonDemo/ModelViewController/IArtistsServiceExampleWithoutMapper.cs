//MVC models

//Controller or Services

namespace ModelViewController
{
    using System;
    using System.Collections.Generic;

    using DataTransferObects;

    public interface IArtistsServiceExampleWithoutMapper
    {
        IEnumerable<ArtistWithCountWithSmallProperties> GetAllWithCountDTOWithSmallProperties();

        IEnumerable<ArtistWithCountWithBigProperties> GetAllWithCountDTOWithBigProperties();


    }
}
