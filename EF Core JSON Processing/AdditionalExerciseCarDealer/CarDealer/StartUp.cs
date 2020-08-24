using CarDealer.Core;

namespace CarDealer
{
    class StartUp
    {
        static void Main(string[] args)
        {
            Engine engine = new Engine();
            engine.ResetDB();
            engine.Run();
        }
    }
}
