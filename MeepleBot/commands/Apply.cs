using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using MeepleBot.database;
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
        var databaseService = new RealmDatabaseService();
        try
        { 
            if (!await databaseService.ApplicationExists(context.User.Id.ToString())) 
            {
                await databaseService.CreateApplication(context.User.Id.ToString(), game, username);
            }
            else { await context.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("You have already applied.")); return; }
             
            await context.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("You have successfully applied"));
            Logging.Logger.LogInfo(Logs.Discord, $"{context.User.Username} ran the /apply command. \nParams: {username}, {game}");
        }
        catch (Exception ex)
        {
            Logging.Logger.LogError(Logs.Discord, $"Error processing /apply command: {ex.Message}");
        }
    }
}