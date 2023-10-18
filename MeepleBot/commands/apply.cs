using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using MeepleBot.objects;

namespace MeepleBot.commands;

public class ApplicationCommand : ApplicationCommandModule
{
    [SlashCommand("apply", "Apply for a game using this command")]
    public async Task Apply(
        InteractionContext context,
        [Option("username", "What your IGN is")]
        string username,
        [Option("game", "What game you want to be whitelisted on")]
        string game
        )
    {
        await context.DeferAsync();
        DateTimeOffset time = new(DateTime.UtcNow);
        string unixTime = time.ToUnixTimeMilliseconds().ToString();
        await realmdb.database.WriteAsync(() =>
        {
            realmdb.database.Add(new Application
            {
                Time = unixTime,
                DiscordId = context.User.Id.ToString(),
                Game = game,
                Username = username,
            }); 
        });
        var response = new DiscordInteractionResponseBuilder()
            .WithContent("You have successfully applied.");
        await context.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("You have successfully applied"));
    }
}