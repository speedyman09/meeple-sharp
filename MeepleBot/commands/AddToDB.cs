using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace MeepleBot.commands;

public class AddToDb : ApplicationCommandModule
{
    [SlashCommand("addToDb", "Adds something to the realm database")]
    static Task addtodb (
        InteractionContext interaction,
        [Option("object", "What to add to the database")]
        string obj
        )
    {
        interaction.CreateResponseAsync(new DiscordInteractionResponseBuilder().WithContent(obj));
        return Task.CompletedTask;
    }
}