using System.Collections.Generic;

public class FSMConfig<T, W> where W : AbstractFSMData
{
    public Dictionary<T, List<T>> configTransitions { get; private set; }
    public Dictionary<T, FSMState<W>> statesDatabase { get; private set; }

    public FSMConfig()
    {
        this.Init();
    }

    public FSMConfig(FSMStateLinksData<T> configData, Dictionary<T, FSMState<W>> statesData)
    {
        this.Init();
        this.ConfigureStates(statesData);
        this.ConfigureConnections(configData);
    }

    public void FSMConfigUtil<R>(FSMStateLinksData<T> configData, Dictionary<T, R> statesData) where R : FSMState<W>
    {
        this.ConfigureStates(statesData);
        this.ConfigureConnections(configData);
    }

    public virtual void ConfigureConnections(FSMStateLinksData<T> configData)
    {
        foreach (FSMStateLink<T> stateConn in configData.linksData)
            this.SetTransition(stateConn.stateFrom, stateConn.stateTo);
    }

    public virtual void ConfigureStates(Dictionary<T, FSMState<W>> statesData)
    {
        foreach (KeyValuePair<T, FSMState<W>> state in statesData)
            this.SetState(state.Key, state.Value);
    }

    public virtual void ConfigureStates<R>(Dictionary<T, R> statesData) where R : FSMState<W>
    {
        foreach (KeyValuePair<T, R> state in statesData)
            this.SetState(state.Key, state.Value);
    }

    public void Init()
    {
        this.configTransitions = new Dictionary<T, List<T>>();
        this.statesDatabase = new Dictionary<T, FSMState<W>>();
    }

    public FSMState<W> GetStateFromTransition(T from, T to)
    {
        List<T> possibleStates;
        this.configTransitions.TryGetValue(from, out possibleStates);

        if (null != possibleStates && possibleStates.Contains(to))
            return this.statesDatabase[to];

        return null;
    }

    public FSMState<W> GetState(T stateType)
    {
        FSMState<W> state;
        this.statesDatabase.TryGetValue(stateType, out state);
        return state;
    }

    public T GetTypeByState(FSMState<W> stateType)
    {
        foreach (var state in this.statesDatabase)
        {
            if (state.Value == stateType)
                return state.Key;
        }

        return default(T);
    }

    public void SetState(T stateKey, FSMState<W> fSMState)
    {
        this.statesDatabase[stateKey] = fSMState;
    }

    public void RemoveState(T stateKey, FSMState<W> fSMState)
    {
        this.statesDatabase.Remove(stateKey);
    }

    public void SetTransition(T from, T to)
    {
        List<T> transitions = null;
        this.configTransitions.TryGetValue(from, out transitions);
        if (null == transitions)
            this.configTransitions[from] = new List<T>() { to };
        else
        {
            transitions.Add(to);
            this.configTransitions[from] = transitions;
        }
    }

    public void RemoveTransition(T from, T to)
    {
        List<T> transitions = null;
        this.configTransitions.TryGetValue(from, out transitions);
        if (null != transitions)
        {
            transitions.Remove(to);
            this.configTransitions[from] = transitions;
        }
    }

    public static FSMStateLink<T> StateToConfig(T from, T to)
    {
        FSMStateLink<T> config = new FSMStateLink<T>(from, to);
        return config;
    }
}