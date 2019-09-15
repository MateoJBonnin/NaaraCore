using System;
using System.Collections.Generic;
using System.Linq;

public class GenericFSM<T, W> where W : AbstractFSMData
{
    public event Action<FSMState<W>, FSMState<W>> OnStateChanged;
    public AbstractFSMTransitioner<T, W> FSMTransitioner { get; set; }
    public FSMConfig<T, W> FSMConfig { get; set; }

    public T GetCurrentType
    {
        get
        {
            return this.currentType;
        }
    }

    public FSMState<W> GetCurrentState
    {
        get
        {
            return this.currentState;
        }
    }

    private FSMState<W> currentState;
    private T currentType;

    public GenericFSM()
    {
        this.FSMConfig = new FSMConfig<T, W>();
    }

    public GenericFSM(FSMConfig<T, W> config, AbstractFSMTransitioner<T, W> FSMTransitioner)
    {
        this.SetConfig(config);
        this.SetTransitioner(FSMTransitioner);
    }

    public void SetConfig(FSMConfig<T, W> config)
    {
        this.FSMConfig = config;
    }

    public virtual void SetTransitioner(AbstractFSMTransitioner<T, W> abstractFSMTransitioner)
    {
        this.FSMTransitioner = abstractFSMTransitioner;
    }

    public void Feed(T state, W data = null)
    {
        FSMState<W> newState = null;
        if (this.currentState == null)
            newState = this.FSMConfig.statesDatabase[state];
        else
            newState = this.FSMTransitioner.TransitionateState(this.FSMConfig.GetTypeByState(currentState), state);

        if (null != newState)
        {
            if (this.currentState != newState)
            {
                this.OnStateChanged?.Invoke(this.currentState, newState);
                this.currentState?.OnExit();
                this.currentType = state;
                this.currentState = newState;
                this.currentState.Feed(data);
                this.currentState.OnEnter();
            }
            else
                this.currentState?.Feed(data);
        }
    }

    public virtual void Update()
    {
        this.currentState?.OnUpdate();
    }

    public List<FSMState<W>> GetAllStates()
    {
        return this.FSMConfig.statesDatabase.Values.ToList();
    }

    public T GetTypeByState(FSMState<W> state)
    {
        T type;
        var invertedStates = this.FSMConfig.statesDatabase.ToDictionary(x => x.Value, x => x.Key);
        invertedStates.TryGetValue(state, out type);
        return type;
    }
}
