public class EmptyGenericFSM<Key> : GenericFSM<Key, EmptyFSMStateData>
{
    public EmptyGenericFSM(EmptyFSMStateDatabase<Key> fSMStateDatabase, AbstractFSMTransitioner<Key, EmptyFSMStateData> fSMTransitioner) :
        base(fSMStateDatabase, fSMTransitioner)
    {
    }
}

public class EmptyGenericFSMCustomState<State, Key> : GenericFSMCustomState<EmptyFSMStateDatabaseCustomState<State, Key>, AbstractFSMTransitionerCustomState<State, Key, EmptyFSMStateData>, State, Key, EmptyFSMStateData> where State : FSMState<EmptyFSMStateData>
{
    public EmptyGenericFSMCustomState(EmptyFSMStateDatabaseCustomState<State, Key> fSMStateDatabase, AbstractFSMTransitionerCustomState<State, Key, EmptyFSMStateData> fSMTransitioner) :
        base(fSMStateDatabase, fSMTransitioner)
    {
    }
}
