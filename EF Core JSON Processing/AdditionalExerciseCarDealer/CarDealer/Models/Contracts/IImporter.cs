using CarDealer.Data;

namespace CarDealer.Models.Contracts
{
    public interface IImporter
    {        
        void Import(CarDealerContext context, string fileName);
    }
}
