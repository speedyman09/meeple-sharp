using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using MeepleBot.database;

namespace MeepleBot.commands;

public class BenchmarkCommand : ApplicationCommand
{
    [SlashCommand("benchmark","This command allows the developers to benchmark how fast the bot is")]
    public async Task Benchmark(InteractionContext context)
    {
        await context.DeferAsync();
        var realm = new RealmDatabaseService();
        var members = context.Guild.Members;
        
        var startTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
        foreach (var member in members.Values)
        {
            await realm.AddUser(member.Id.ToString(), member.Username);
        }
        var endTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
        await context.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"Added {members.Count} users to the database, took {endTime - startTime} ms"));
    }
}