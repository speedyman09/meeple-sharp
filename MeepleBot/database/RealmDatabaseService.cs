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
}