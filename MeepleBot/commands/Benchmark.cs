using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using MeepleBot.database;
using MeepleBot.objects;

namespace MeepleBot.commands;

public class BenchmarkCommand : ApplicationCommand
{
    [SlashCommand("benchmark", "This command allows the developers to benchmark how fast the bot is")]
    public async Task Benchmark(InteractionContext context)
    {
        await context.DeferAsync();
        
        var realm = new RealmDatabaseService();
        var members = context.Guild.Members;

        var startTime = DateTimeOffset.UtcNow;

        var usersToAdd = members.Values
            .Select(member => new UserObject { DiscordId = member.Id.ToString(), Username = member.Username })
            .ToList(); 
        await realm.AddUsers(usersToAdd); 

        var endTime = DateTimeOffset.UtcNow;
        var elapsedMilliseconds = Math.Round((endTime - startTime).TotalMilliseconds);

        await context.FollowUpAsync(new DiscordFollowupMessageBuilder()
            .WithContent($"Added {members.Count} users to the database, took {elapsedMilliseconds} ms"));
    }
}