using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using MeepleBot.database;
using MeepleBot.objects;
using Realms;

namespace MeepleBot.commands;

public class NotifyCommand : ApplicationCommand
{
    [SlashCommand("notify", "Notify a user")]
    public async Task Notify(
        InteractionContext context,
        [Option("user", "The user you want to notify")]
        DiscordUser user1
        )
    {
        await context.DeferAsync(ephemeral: true);
        
        var databaseService = new RealmDatabaseService();
        var userApplication = await databaseService.GetUserApplication(user1.Id.ToString());
        
        if (userApplication != null)
        {
            await databaseService.AcceptUser(userApplication);
        }
        else
        {
            await context.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("User has not applied or has already been accepted"));
            return;
        }

        await context.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"Notifying <@{userApplication.DiscordId}>"));
        await context.Channel.SendMessageAsync(new DiscordMessageBuilder().WithContent($"<@{user1.Id}>, you have been whitelisted.").WithAllowedMention(new UserMention(user1))); 
        // The "WithAllowedMentions" is necessary since discord does not allow bots to mention users by default
        Logging.Logger.LogInfo(Logs.Discord, $"{context.User.Username} ran the /notify command. \nParams: {user1}");
    }
}