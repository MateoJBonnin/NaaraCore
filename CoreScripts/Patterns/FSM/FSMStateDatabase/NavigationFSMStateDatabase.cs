using System.Collections.Generic;

public class NavigationFSMStateDatabase<Key> : DefaultFSMStateDatabase<Key, EntityNavigationFSMStateData>
{
    public NavigationFSMStateDatabase(Dictionary<Key, FSMState<EntityNavigationFSMStateData>> statesDatabase) : base(statesDatabase)
    {
    }
}