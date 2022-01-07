using System;
using DesktopApp.Service;

namespace DesktopApp.Util
{
    public class ConsoleErrorHandler : IErrorHandler
    {
        public void OnError(string message)
        {
            Console.WriteLine(message);
        }
    }
}