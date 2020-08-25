namespace CarDealer.Contracts
{
    public interface IExporter
    {
        void Export<T>(string filePath, T[] collection);

        void Export<T>(string filePath, T model);
    }
}
