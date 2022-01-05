public class FSMRestrictedTransitioner<Key, Data> : FSMRestrictedTransitionerCustomState<AbstractFSMStateDatabaseCustomState<IFSMState<Data>, Key, Data>, AbstractFSMTransitionsConfigCustomState<AbstractFSMStateDatabaseCustomState<IFSMState<Data>, Key, Data>, IFSMState<Data>, Key, Data>, IFSMState<Data>, Key, Data> where Data : AbstractFSMData
{
    public FSMRestrictedTransitioner(AbstractFSMTransitionsConfigCustomState<AbstractFSMStateDatabaseCustomState<IFSMState<Data>, Key, Data>, IFSMState<Data>, Key, Data> abstractFSMConfig) : base(abstractFSMConfig)
    {
    }
}

public class FSMRestrictedTransitionerCustomState<Database, TransitionConfig, State, Key, Data> : AbstractFSMTransitionerCustomState<State, Key, Data> where Data : AbstractFSMData where State : IFSMState<Data> where Database : AbstractFSMStateDatabaseCustomState<State, Key, Data> where TransitionConfig : AbstractFSMTransitionsConfigCustomState<Database, State, Key, Data>
{
    protected TransitionConfig transitionConfig;

    public FSMRestrictedTransitionerCustomState(TransitionConfig transitionConfig)
    {
        this.transitionConfig = transitionConfig;
    }

    public override State TransitionateState(Key fromState, Key toState)
    {
        return this.transitionConfig.GetStateFromTransition(fromState, toState);
    }
}

public class EmptyFSMRestrictedTransitioner<Key> : FSMRestrictedTransitionerCustomState<EmptyFSMStateDatabaseCustomState<IFSMState<EmptyFSMStateData>, Key>, EmptyFSMTransitionsConfig<Key>, IFSMState<EmptyFSMStateData>, Key, EmptyFSMStateData>
{
    public EmptyFSMRestrictedTransitioner(EmptyFSMTransitionsConfig<Key> abstractFSMConfig) : base(abstractFSMConfig)
    {
    }
}

/*
public class NoKeyFSMRestrictedTransitionerCustomState<State, Data> : FSMRestrictedTransitionerCustomState<NoKeyFSMStateDatabaseCustomState<Data>, NoKeyFSMTransitionConfigCustomState<State, Data>, State, State, Data> where State : IFSMState<Data> where Data : AbstractFSMData
{
    public NoKeyFSMRestrictedTransitionerCustomState(NoKeyFSMTransitionConfigCustomState<State, Data> transitionConfig) : base(transitionConfig)
    {
    }
}*/
