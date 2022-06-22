using Meets.BL.Entities;
using Meets.UI;

namespace Meets
{ 
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = new Host();
            host.Run();

            Console.ReadKey();
        }
    }
}