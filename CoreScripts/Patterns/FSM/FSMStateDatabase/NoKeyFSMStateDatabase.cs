using System.Collections.Generic;

public class NoKeyFSMStateDatabase<T> : DefaultFSMStateDatabase<FSMState<T>, T> where T : AbstractFSMData
{

    public NoKeyFSMStateDatabase() : base()
    {
    }

    public NoKeyFSMStateDatabase(Dictionary<FSMState<T>, FSMState<T>> states) : base(states)
    {
    }
}