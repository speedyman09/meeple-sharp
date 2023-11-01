using DSharpPlus.Entities;
using MeepleBot.objects;
using Realms;

namespace MeepleBot.database;

public class RealmDatabaseService
{
    private readonly Realm _realm;

    public RealmDatabaseService()
    {
        var config = new RealmConfiguration("meeplebot.realm")
        {
            SchemaVersion = 1,
        };
        _realm = Realm.GetInstance(config);
    }
    public async Task CreateApplication(string id, string game, string username)
    {
        DateTimeOffset time = new(DateTime.UtcNow);
        string unixTime = time.ToUnixTimeMilliseconds().ToString();
        await _realm.WriteAsync(() =>
        {
            _realm.Add(new Application
            {
                Time = unixTime,
                DiscordId = id,
                Game = game,
                Username = username,
            });
        });
    }

    public Task<bool> ApplicationExists(string id)
    {
        return Task.FromResult(_realm.All<Application>().Any(application => application.DiscordId == id));
    }
    
}