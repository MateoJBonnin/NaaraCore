using System.Collections.Generic;

public class NoKeyFSMStateDatabase<Data> : NoKeyFSMStateDatabaseCustomState<FSMState<Data>, Data> where Data : AbstractFSMData
{
    public NoKeyFSMStateDatabase(Dictionary<FSMState<Data>, FSMState<Data>> states) : base(states)
    {
    }
}

public class NoKeyFSMStateDatabaseCustomState<State, Data> : DefaultFSMStateDatabaseCustomState<State, State, Data> where Data : AbstractFSMData where State : FSMState<Data>
{
    public NoKeyFSMStateDatabaseCustomState(Dictionary<State, State> states) : base(states)
    {
    }
}

public class SimpleNoKeyFSMStateDatabaseCustomState<State, Data> : DefaultFSMStateDatabaseCustomState<State, State, Data> where Data : AbstractFSMData where State : FSMState<Data>
{
    public SimpleNoKeyFSMStateDatabaseCustomState(Dictionary<State, State> states) : base(states)
    {
    }
}