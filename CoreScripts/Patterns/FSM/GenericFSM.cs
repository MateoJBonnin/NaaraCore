using System;

public class GenericFSM<T, W> : AbstractFSM<T, W> where W : AbstractFSMData
{
    public override event Action<FSMState<W>, FSMState<W>> OnStateChanged;

    public GenericFSM(AbstractFSMStateDatabase<T, W> fSMStateDatabase, AbstractFSMTransitioner<T, W> fSMTransitioner) :
        base(fSMStateDatabase, fSMTransitioner)
    {
    }

    public override void Feed(T state, W data = null)
    {
        if (this.FSMStateDatabase.ContainsState(state))
        {
            FSMState<W> newState = null;
            if (this.GetCurrentState == null)
                newState = this.FSMStateDatabase.GetStateByType(state);
            else
                newState = this.FSMTransitioner.TransitionateState(this.GetCurrentType, state);

            if (null != newState)
            {
                if (this.GetCurrentState != newState)
                {
                    this.OnStateChanged?.Invoke(this.GetCurrentState, newState);
                    this.GetCurrentState?.OnExit();
                    this.GetCurrentType = state;
                    this.GetCurrentState = newState;
                    this.GetCurrentState.Feed(data);
                    this.GetCurrentState.OnEnter();
                }
                else
                    this.GetCurrentState?.Feed(data);
            }
        }
    }

    public virtual void Update()
    {
        this.GetCurrentState?.OnUpdate();
    }
}