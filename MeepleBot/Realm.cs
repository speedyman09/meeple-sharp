using Realms;

namespace MeepleBot;

public static class realmdb
{
    public static readonly Realm realmdatabase;

    static realmdb()
    {
        realmdatabase = Realm.GetInstance();
    }
}