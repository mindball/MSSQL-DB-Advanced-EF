//MVC models

//Controller or Services

namespace ModelViewController
{
    using System;
    using System.Collections.Generic;

    using DataTransferObects;

    public interface IArtistsService
    {
        IEnumerable<ArtistWithCount> GetAllWithCount();
    }
}
