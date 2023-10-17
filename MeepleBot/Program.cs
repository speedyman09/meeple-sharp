using DSharpPlus;
using DSharpPlus.SlashCommands;
using MeepleBot.commands;

namespace MeepleBot;

static class MeepleBot
{
    static async Task Main()
    {
        DiscordClient client = new(new DiscordConfiguration()
        {
            Token = (await GetToken()),
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.Guilds,
        });
        await client.ConnectAsync();
        Logging.Instance.Logger.LogInfo(Logs.Discord, $"Logged in as {client.CurrentUser.Username}");
        await RegisterCommands(client);
        Logging.Instance.Logger.LogInfo(Logs.Discord, "Slash commands registered");
        await Task.Delay(-1);
    }

    static Task<String> GetToken()
    {
        return Task.Run(() =>
        {
            try
            {
                return File.ReadAllText("token.txt");
            }
            catch (FileNotFoundException)
            {
                Logging.Instance.Logger.LogCritical(Logs.Token, "token.txt was not found, exiting");
                Task.Delay(1000);
                Environment.Exit(1);
            }
            return "";
        });
    }

    static Task RegisterCommands(DiscordClient client)
    {
        var slash = client.UseSlashCommands();
        slash.RegisterCommands<AddToDb>();
        return Task.CompletedTask;
    }
}