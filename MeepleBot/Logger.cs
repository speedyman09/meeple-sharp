using NotEnoughLogs;
using NotEnoughLogs.Behaviour;

namespace MeepleBot;

public sealed class Logging
{
   private static readonly Lazy<Logging> _instance = new(() => new Logging());
   public static Logging Instance => _instance.Value;
   public Logger Logger { get; }
   private Logging()
   {
      var configuration = new LoggerConfiguration
      {
         Behaviour = new DirectLoggingBehaviour(),
         MaxLevel = LogLevel.Trace
      };
      Logger = new Logger(configuration);
   }
}

enum Logs
{
   Token,
   DiscordError
}