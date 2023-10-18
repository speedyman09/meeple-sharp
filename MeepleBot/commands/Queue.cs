using System.Text;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using MeepleBot.objects;
using Realms;

namespace MeepleBot.commands;

public class QueueCommand : ApplicationCommand
{   
    [SlashCommand("queue", "Returns the queue for a game")]
    public async Task List(
        InteractionContext context,
        [Option("game", "The game you want to view the queue of")]
        [Choice("Astroneer", "astroneer")]
        string game
        )
    {
        await context.DeferAsync();
        
       var localRealm = Realm.GetInstance();
       var applications = localRealm.All<Application>().Where(application => application.Game == game && application.Accepted == false);

       var responseBuilder = new StringBuilder($"Queue for {game}:\n");
       foreach (var application in applications)
       {
           responseBuilder.AppendLine($"<@{application.DiscordId}> - {application.Username}\n");
       }
       
       await context.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent(responseBuilder.ToString()));
       Logging.Logger.LogInfo(Logs.Discord, $"{context.User.Username} ran the /queue command. \nParams: {game}");
    }
}