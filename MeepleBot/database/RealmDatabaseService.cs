using DSharpPlus.Entities;
using MeepleBot.objects;
using Realms;
using Realms.Sync;

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

    public Task<Application?> GetUserApplication(string id)
    {
        var application = _realm.All<Application>()
            .FirstOrDefault(application => application.DiscordId == id && application.Accepted == false);
        return Task.FromResult(application);
    }
    public Task<IQueryable<Application>> GetAllApplications(string game)
    {
        var application = _realm.All<Application>().Where(application => application.Game == game && application.Accepted == false);
        return Task.FromResult(application);
    }
    public async Task AcceptUser(Application userApplication)
    {
        using var transaction = await _realm.BeginWriteAsync();
        userApplication.Accepted = true;
        await transaction.CommitAsync();
    }
}