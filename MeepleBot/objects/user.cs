using Realms;

namespace MeepleBot.objects;

public class UserObject : RealmObject
{
    [MapTo("discordid")] 
    public string? DiscordId { get; set; }
    [MapTo("username")]  
    public string? Username { get; set; }
}