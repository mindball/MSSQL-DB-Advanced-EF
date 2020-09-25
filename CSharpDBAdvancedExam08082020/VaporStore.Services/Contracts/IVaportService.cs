using System;
using System.Collections.Generic;
using System.Text;

namespace VaporStore.Services.Contracts
{
    public interface IVaportService
    {
        string Create(string name, 
            decimal price, 
            DateTime releaseDate, 
            string developer, 
            string genre, 
            List<string> tags);
    }
}
