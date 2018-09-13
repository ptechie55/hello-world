
namespace HelloWorldInfrastructure.FrameworkWrappers
{
    public interface IConsole
    {
        void Write(string message);
        void WriteLine(string message);
        void ErrorWrite(string message);
        void ErrorWriteLine(string message);
    }
}