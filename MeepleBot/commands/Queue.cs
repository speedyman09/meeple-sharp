using System.Text;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using MeepleBot.database;
using MeepleBot.objects;
using Realms;

namespace MeepleBot.commands;

public class QueueCommand : ApplicationCommand
{   
    [SlashCommand("queue", "Returns the queue for a game")]
    public async Task Queue(
        InteractionContext context,
        [Option("game", "The game you want to view the queue of")]
        [Choice("Astroneer", "astroneer")]
        string game
        )
    {
        await context.DeferAsync(ephemeral: true);
        
       var databaseService = new RealmDatabaseService();
       var applications = await databaseService.GetApplications(game);

       var responseBuilder = new StringBuilder($"Queue for {game}:\n");
       foreach (var application in applications)
       {
           responseBuilder.AppendLine($"<@{application.DiscordId}> applied at <t:{Convert.ToInt64(application.Time)/1000}:t>```{application.Username}```"); // <t: x :t> is discord timestamp, this accounts for different timezones
       }
       
       await context.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent(responseBuilder.ToString()));
       Logging.Logger.LogInfo(Logs.Discord, $"{context.User.Username} ran the /queue command. \nParams: {game}");
    }
}