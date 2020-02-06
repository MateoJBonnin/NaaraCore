using System.Collections.Generic;

public class NoTransitionerSimpleNoKeyFSM<State> : SimpleNoKeyFSMCustomState<State, EmptyFSMStateData> where State : FSMState<EmptyFSMStateData>
{
    public NoTransitionerSimpleNoKeyFSM() : base(new NoKeyFSMStateDatabaseCustomState<State, EmptyFSMStateData>(new Dictionary<State, State>()))
    {
    }

    public override void Feed(State state, EmptyFSMStateData data = null)
    {
        this.GetCurrentState?.OnExit();
        this.GetCurrentType = state;
        this.GetCurrentState = state;
        this.GetCurrentState.Feed(data);
        this.GetCurrentState.OnEnter();
    }
}