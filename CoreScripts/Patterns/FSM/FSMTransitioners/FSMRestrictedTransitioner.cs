public class FSMRestrictedTransitioner<Key, Data> : FSMRestrictedTransitionerCustomState<AbstractFSMStateDatabaseCustomState<FSMState<Data>, Key, Data>, AbstractFSMTransitionsConfigCustomState<AbstractFSMStateDatabaseCustomState<FSMState<Data>, Key, Data>, FSMState<Data>, Key, Data>, FSMState<Data>, Key, Data> where Data : AbstractFSMData
{
    public FSMRestrictedTransitioner(AbstractFSMTransitionsConfigCustomState<AbstractFSMStateDatabaseCustomState<FSMState<Data>, Key, Data>, FSMState<Data>, Key, Data> abstractFSMConfig) : base(abstractFSMConfig)
    {
    }
}

public class FSMRestrictedTransitionerCustomState<Database, TransitionConfig, State, Key, Data> : AbstractFSMTransitionerCustomState<State, Key, Data> where Data : AbstractFSMData where State : FSMState<Data> where Database : AbstractFSMStateDatabaseCustomState<State, Key, Data> where TransitionConfig : AbstractFSMTransitionsConfigCustomState<Database, State, Key, Data>
{
    protected TransitionConfig transitionConfig;

    public FSMRestrictedTransitionerCustomState(TransitionConfig transitionConfig)
    {
        this.transitionConfig = transitionConfig;
    }

    public override State TransitionateState(Key fromState, Key toState)
    {
        return transitionConfig.GetStateFromTransition(fromState, toState);
    }
}

public class EmptyFSMRestrictedTransitioner<Key> : FSMRestrictedTransitionerCustomState<EmptyFSMStateDatabaseCustomState<FSMState<EmptyFSMStateData>, Key>, EmptyFSMTransitionsConfig<Key>, FSMState<EmptyFSMStateData>, Key, EmptyFSMStateData>
{
    public EmptyFSMRestrictedTransitioner(EmptyFSMTransitionsConfig<Key> abstractFSMConfig) : base(abstractFSMConfig)
    {
    }
}


public class NoKeyFSMRestrictedTransitionerCustomState<State, Data> : FSMRestrictedTransitionerCustomState<NoKeyFSMStateDatabaseCustomState<State, Data>, NoKeyFSMTransitionConfigCustomState<State, Data>, State, State, Data> where State : FSMState<Data> where Data : AbstractFSMData
{
    public NoKeyFSMRestrictedTransitionerCustomState(NoKeyFSMTransitionConfigCustomState<State, Data> transitionConfig) : base(transitionConfig)
    {
    }
}