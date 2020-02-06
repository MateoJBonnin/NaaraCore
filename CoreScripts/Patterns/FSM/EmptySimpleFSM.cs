﻿public class EmptySimpleFSM<Key> : SimpleFSMCustomState<EmptyFSMStateDatabase<Key>, FSMState<EmptyFSMStateData>, Key, EmptyFSMStateData>
{
    public EmptySimpleFSM(EmptyFSMStateDatabase<Key> fSMStateDatabase) : base(fSMStateDatabase)
    {
    }
}

public class EmptySimpleFSMCustomState<Database, State, Key> : SimpleFSMCustomState<Database, State, Key, EmptyFSMStateData> where State : FSMState<EmptyFSMStateData> where Database : AbstractFSMStateDatabaseCustomState<State, Key, EmptyFSMStateData>
{
    public EmptySimpleFSMCustomState(Database fSMStateDatabase) : base(fSMStateDatabase)
    {
    }
}