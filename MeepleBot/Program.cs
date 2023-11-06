using DSharpPlus;
using DSharpPlus.SlashCommands;
using MeepleBot.commands;

namespace MeepleBot;

static class MeepleBot
{
    private static async Task Main()
    {
        DiscordClient client = new(new DiscordConfiguration()
        {
            Token = (await GetToken()),
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.All,
        });
        await client.ConnectAsync();
        Logging.Logger.LogInfo(Logs.Discord, $"Logged in as {client.CurrentUser.Username}");
        await RegisterCommands(client);
        Logging.Logger.LogInfo(Logs.Discord, "Slash commands registered");
        await Task.Delay(-1);
    }

    private static Task<String> GetToken()
    {
        return Task.Run(() =>
        {
            try
            {
                return File.ReadAllText("token.txt");
            }
            catch (FileNotFoundException)
            {
                Logging.Logger.LogCritical(Logs.Token, "token.txt was not found, exiting");
                Task.Delay(1000);
                Environment.Exit(1);
            }
            return "";
        });
    }

    private static Task RegisterCommands(DiscordClient clientContext)
    {
        var slash = clientContext.UseSlashCommands();
        slash.RegisterCommands<ApplicationCommand>();
        slash.RegisterCommands<QueueCommand>();
        slash.RegisterCommands<NotifyCommand>();
        slash.RegisterCommands<BenchmarkCommand>();
        return Task.CompletedTask;
    }
}