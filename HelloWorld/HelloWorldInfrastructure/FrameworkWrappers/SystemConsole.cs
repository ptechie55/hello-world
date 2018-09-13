using System;
namespace HelloWorldInfrastructure.FrameworkWrappers
{
    public class SystemConsole : IConsole
    {
        public void Write(string message)
        {
            Console.Write(message);
        }
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
        public void ErrorWrite(string message)
        {
            Console.Error.Write(message);
        }
        public void ErrorWriteLine(string message)
        {
            Console.Error.WriteLine(message);
        }
    }
}