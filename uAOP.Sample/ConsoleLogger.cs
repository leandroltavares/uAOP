using System;
using uAOP.Core.Interfaces;

namespace uAOP.Sample
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
