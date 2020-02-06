public class EmptyGenericFSM<Key> : EmptyGenericFSMCustomState<FSMState<EmptyFSMStateData>, Key>
{
    public EmptyGenericFSM(EmptyFSMStateDatabaseCustomState<FSMState<EmptyFSMStateData>, Key> fSMStateDatabase, AbstractFSMTransitionerCustomState<FSMState<EmptyFSMStateData>, Key, EmptyFSMStateData> fSMTransitioner) :
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