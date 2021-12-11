using System;

public class GenericFSM<Key, Data> : GenericFSMCustomState<AbstractFSMStateDatabase<Key, Data>, AbstractFSMTransitioner<Key, Data>, FSMState<Data>, Key, Data> where Data : AbstractFSMData
{
    public override event Action<FSMState<Data>, FSMState<Data>> OnStateChanged;

    public GenericFSM(AbstractFSMStateDatabase<Key, Data> fSMStateDatabase, AbstractFSMTransitioner<Key, Data> fSMTransitioner) :
        base(fSMStateDatabase, fSMTransitioner)
    {
    }
}

public class GenericFSMCustomState<Database, Transitioner, State, Key, Data> : AbstractFSMCustom<Database, Transitioner, State, Key, Data> where Data : AbstractFSMData where State : FSMState<Data> where Database : AbstractFSMStateDatabaseCustomState<State, Key, Data> where Transitioner : AbstractFSMTransitionerCustomState<State, Key, Data>
{
    public override event Action<State, State> OnStateChanged;

    public GenericFSMCustomState(Database database, Transitioner transitioner) :
        base(database, transitioner)
    {
    }

    public override void Feed(Key state, Data data = null)
    {
        if (this.FSMStateDatabase.ContainsState(state))
        {
            State newState = null;
            if (this.CurrentState == null)
                newState = this.FSMStateDatabase.GetStateByType(state);
            else
                newState = this.FSMTransitioner.TransitionateState(this.CurrentType, state);

            if (null != newState)
            {
                if (this.CurrentState != newState)
                {
                    this.OnStateChanged?.Invoke(this.CurrentState, newState);
                    this.CurrentState?.OnExit();
                    this.CurrentType = state;
                    this.CurrentState = newState;
                    this.CurrentState.Feed(data);
                    this.CurrentState.OnEnter();
                }
                else
                    this.CurrentState?.Feed(data);
            }
        }
    }

    public virtual void Update()
    {
        this.CurrentState?.OnUpdate();
    }
}