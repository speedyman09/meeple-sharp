using Realms;

namespace MeepleBot;

public static class realmdb
{
    public static readonly Realm database;

    static realmdb()
    {
        database = Realm.GetInstance();
    }
}