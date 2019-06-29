using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenericFSM<T> where T : Enum
{
    public AbstractFSMTransitioner<T> FSMTransitioner { get; set; }
    public FSMConfig<T> FSMConfig { get; set; }
    public Action<FSMState, FSMState> OnStateChanged;

    public T GetCurrentType
    {
        get
        {
            return this.currentType;
        }
    }

    public FSMState GetCurrentState
    {
        get
        {
            return this.currentState;
        }
    }

    private FSMState currentState;
    private T currentType;

    public GenericFSM()
    {
        this.FSMConfig = new FSMConfig<T>();
    }

    public GenericFSM(FSMConfig<T> config, AbstractFSMTransitioner<T> FSMTransitioner)
    {
        this.SetConfig(config);
        this.SetTransitioner(FSMTransitioner);
    }

    public void SetConfig(FSMConfig<T> config)
    {
        this.FSMConfig = config;
    }

    public virtual void SetTransitioner(AbstractFSMTransitioner<T> abstractFSMTransitioner)
    {
        this.FSMTransitioner = abstractFSMTransitioner;
    }

    public void Feed(T state, JSONObject data = null)
    {
        FSMState newState = null;
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
                this.currentState.SetData(data);
                this.currentState.OnEnter();
            }
            else
                this.currentState?.SetData(data);
        }
    }

    public void Update()
    {
        this.currentState?.OnUpdate();
    }

    public List<FSMState> GetAllStates()
    {
        return this.FSMConfig.statesDatabase.Values.ToList();
    }

    public T GetTypeByState(FSMState state)
    {
        T type;
        var invertedStates = this.FSMConfig.statesDatabase.ToDictionary(x => x.Value, x => x.Key);
        invertedStates.TryGetValue(state, out type);
        return type;
    }
}
