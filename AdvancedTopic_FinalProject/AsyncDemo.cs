using System.Drawing;
using System.Text;

namespace AdvancedTopicsAuthDemo
{
    public class AsyncDemo
    {
        public async Task<string> StartGettingString()
        {
            Console.WriteLine("Looking for string");
            Thread.Sleep(3000);
            return "Found it!";
        }

        public async Task<string> GetString()
        {
            Console.WriteLine("I'm going to start now");
            Task<string> getStringTask = StartGettingString();
            Console.WriteLine("I'm waiting for the string");
            string finished = await getStringTask;
            return finished;
        }
    }
}
