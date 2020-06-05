using Serilog;
using StudentPerfomace.Interfaces;

namespace StudentPerfomace
{
    public class Logger : ICustomLogger
    {
        const string LogPath = @"..\..\..\..\log.txt";

        public Serilog.Core.Logger Setup()
        {
            return new LoggerConfiguration()
                .WriteTo.File(LogPath)
                .WriteTo.Console().
                CreateLogger();
        }
    }
}