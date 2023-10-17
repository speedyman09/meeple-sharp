using DSharpPlus;
using System.Threading.Tasks;
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
        await Task.Delay(-1);
    }

    static Task<String> GetToken()
    {
        var token = String.Empty;
        try
        {
            token = File.ReadAllText("token.txt");
        }
        catch (FileNotFoundException ex)
        {
            Logging.Instance.Logger.LogCritical(Logs.Token, "token.txt was not found, exiting");
            Task.Delay(1000);
            Environment.Exit(1);
        }

        return Task.Run(() => token);
    }
}