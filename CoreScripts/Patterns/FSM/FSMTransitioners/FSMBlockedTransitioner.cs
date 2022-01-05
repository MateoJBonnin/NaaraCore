using System;

public class FSMBlockedTransitioner<T, W> : AbstractFSMTransitioner<T, W> where T : Enum where W : AbstractFSMData
{
    public override IFSMState<W> TransitionateState(T fromState, T toState)
    {
        return null;
    }
}