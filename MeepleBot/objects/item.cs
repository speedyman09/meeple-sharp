using Realms;

namespace MeepleBot.objects;

public partial class Item : IRealmObject
{
    [MapTo("text")]
    public string text { get; set; }
}