using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using MeepleBot.objects;
using Realms;

namespace MeepleBot.commands;

public class ApplicationCommand : ApplicationCommandModule
{
    [SlashCommand("apply", "Apply for a game using this command")]
    public async Task Apply(
        InteractionContext context,
        [Option("username", "What your IGN is")]
        string username,
        [Option("game", "What game you want to be whitelisted on")]
        [Choice("Astroneer", "astroneer")]
        string game
    )
    {
        await context.DeferAsync();
        DateTimeOffset time = new(DateTime.UtcNow);
        string unixTime = time.ToUnixTimeMilliseconds().ToString();

        try
        {
            using (var localRealm = await Realm.GetInstanceAsync())
            {
                if (!localRealm.All<Application>().Any(application => application.DiscordId == context.User.Id.ToString()))
                {
                    await localRealm.WriteAsync(() =>
                    {
                        localRealm.Add(new Application
                        {
                            Time = unixTime,
                            DiscordId = context.User.Id.ToString(),
                            Game = game,
                            Username = username,
                        });
                    });
                }
                else
                {
                    await context.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("You have already applied."));
                    return;
                }
                
            }
            await context.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("You have successfully applied"));
            Logging.Logger.LogInfo(Logs.Discord, $"{context.User.Username} ran the /apply command. \nParams: {username}, {game}");
        }
        catch (Exception ex)
        {
            Logging.Logger.LogError(Logs.Discord, $"Error processing /apply command: {ex.Message}");
        }
    }
}