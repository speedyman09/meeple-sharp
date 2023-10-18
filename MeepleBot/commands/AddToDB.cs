using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using MeepleBot.objects;

namespace MeepleBot.commands;

public class DatabaseCommands : ApplicationCommandModule
{
    [SlashCommand("addToDb", "Adds something to the realm database")]
    static Task addtodb (
        InteractionContext interaction,
        [Option("object", "What to add to the database")]
        string obj
        )
    {
        realmdb.realmdatabase.WriteAsync(() =>
        {
            realmdb.realmdatabase.Add<Item>(new Item
            {
                text = obj
            });
        });
        interaction.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent(obj));
        return Task.CompletedTask;
    }
}