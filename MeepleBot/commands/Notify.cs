using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using MeepleBot.objects;
using Realms;

namespace MeepleBot.commands;

public class NotifyCommand : ApplicationCommand
{
    [SlashCommand("notify", "Notify a user")]
    public async Task notify(
        InteractionContext context,
        [Option("user", "The user you want to notify")]
        DiscordUser user1
        )
    {
        await context.DeferAsync(ephemeral: true);
        
        var localRealm = await Realm.GetInstanceAsync();
        var userApplication = localRealm.All<Application>().FirstOrDefault(application => application.DiscordId == user1.Id.ToString() && application.Accepted == false);
        
        if (userApplication != null)
        {
            using var transaction = await localRealm.BeginWriteAsync();
            userApplication.Accepted = true;
            await transaction.CommitAsync();
        }
        else
        {
            await context.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("User has not applied or has already been accepted"));
            return;
        }

        await context.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"Notifying <@{userApplication.DiscordId}>"));
        await context.Channel.SendMessageAsync(new DiscordMessageBuilder().WithContent($"<@{user1.Id}>, you have been whitelisted.").WithAllowedMention(new UserMention(user1)));
        Logging.Logger.LogInfo(Logs.Discord, $"{context.User.Username} ran the /notify command. \nParams: {user1}");
    }
}