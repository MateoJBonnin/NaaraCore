using System.Collections.Generic;

public class NoTransitionerSimpleNoKeyFSM<State> : SimpleNoKeyFSM<EmptyFSMStateData> where State : IFSMState<EmptyFSMStateData>
{
    public NoTransitionerSimpleNoKeyFSM() : base(new NoKeyFSMStateDatabaseCustomState<EmptyFSMStateData>(new HashSet<IFSMState<EmptyFSMStateData>>()))
    {
    }

    public override void Feed(IFSMState<EmptyFSMStateData> state, EmptyFSMStateData data = null)
    {
        base.Feed(state, data);
        this.CurrentState?.OnExit();
        this.CurrentType = state;
        this.CurrentState = state;
        this.CurrentState.Feed(data);
        this.CurrentState.OnEnter();
    }
}
