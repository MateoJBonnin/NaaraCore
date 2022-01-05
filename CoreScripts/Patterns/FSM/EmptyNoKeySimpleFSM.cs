public class EmptyNoKeySimpleFSM : GenericFSMCustomState<NoKeyFSMStateDatabaseCustomState<AbstractFSMData>, AbstractFSMTransitionerCustomState<IFSMState<AbstractFSMData>,
    IFSMState<AbstractFSMData>, AbstractFSMData>, IFSMState<AbstractFSMData>, IFSMState<AbstractFSMData>, AbstractFSMData>
{
    public EmptyNoKeySimpleFSM(NoKeyFSMStateDatabaseCustomState<AbstractFSMData> database, AbstractFSMTransitionerCustomState<IFSMState<AbstractFSMData>, IFSMState<AbstractFSMData>, AbstractFSMData> transitioner) : base(database, transitioner)
    {
    }
}

public class EmptyNoKeySimpleFSMCustomData<Data> : GenericFSMCustomState<NoKeyFSMStateDatabaseCustomState<Data>, AbstractFSMTransitionerCustomState<IFSMState<Data>,
    IFSMState<Data>, Data>, IFSMState<Data>, IFSMState<Data>, Data> where Data : AbstractFSMData
{
    public EmptyNoKeySimpleFSMCustomData(NoKeyFSMStateDatabaseCustomState<Data> database, AbstractFSMTransitionerCustomState<IFSMState<Data>, IFSMState<Data>, Data> transitioner) : base(database, transitioner)
    {
    }
}


/*public class EmptyNoKeySimpleFSMCustomState<State> : GenericFSMCustomState<NoKeyFSMStateDatabaseCustomState<AbstractFSMData>, AbstractFSMTransitionerCustomState<State,
    State, AbstractFSMData>, State, State, AbstractFSMData> where State : IFSMState<AbstractFSMData>
{
    public EmptyNoKeySimpleFSMCustomState(NoKeyFSMStateDatabaseCustomState<AbstractFSMData> database, AbstractFSMTransitionerCustomState<State, State, AbstractFSMData> transitioner) :
        base(database, transitioner)
    {
    }
}*/
