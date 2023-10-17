using NotEnoughLogs;
using NotEnoughLogs.Behaviour;

namespace MeepleBot;

enum Logs
{
   Token,
   Discord,
}

public static class Logging
{
   public static Logger Logger;

   static Logging()
   {
      var configuration = new LoggerConfiguration
      {
         Behaviour = new DirectLoggingBehaviour(),
         MaxLevel = LogLevel.Info
      };
      Logger = new Logger(configuration);
   }
}