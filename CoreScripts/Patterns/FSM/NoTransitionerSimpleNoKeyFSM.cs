using System.Collections.Generic;

public class NoTransitionerSimpleNoKeyFSM<State> : SimpleNoKeyFSMCustomState<State, EmptyFSMStateData> where State : FSMState<EmptyFSMStateData>
{
    public NoTransitionerSimpleNoKeyFSM() : base(new NoKeyFSMStateDatabaseCustomState<State, EmptyFSMStateData>(new Dictionary<State, State>()))
    {
    }

    public override void Feed(State state, EmptyFSMStateData data = null)
    {
        this.CurrentState?.OnExit();
        this.CurrentType = state;
        this.CurrentState = state;
        this.CurrentState.Feed(data);
        this.CurrentState.OnEnter();
    }
}