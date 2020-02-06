using System.Collections.Generic;
using System.Linq;

public class DefaultFSMStateDatabase<Key, Data> : DefaultFSMStateDatabaseCustomState<FSMState<Data>, Key, Data> where Data : AbstractFSMData
{
    public DefaultFSMStateDatabase(Dictionary<Key, FSMState<Data>> statesDatabase)
    {
        this.ConfigureStates(statesDatabase);
    }
}

public class DefaultFSMStateDatabaseCustomState<State, Key, Data> : AbstractFSMStateDatabaseCustomState<State, Key, Data> where Data : AbstractFSMData where State : FSMState<Data>
{
    protected Dictionary<Key, State> StatesDatabase { get; private set; }

    public DefaultFSMStateDatabaseCustomState()
    {
        this.StatesDatabase = new Dictionary<Key, State>();
    }

    public DefaultFSMStateDatabaseCustomState(Dictionary<Key, State> statesDatabase) : this()
    {
        this.ConfigureStates(statesDatabase);
    }

    public override void ConfigureStates(Dictionary<Key, State> statesData)
    {
        foreach (KeyValuePair<Key, State> state in statesData)
            this.SetState(state.Key, state.Value);
    }

    public override void SetState(Key stateKey, State fSMState)
    {
        this.StatesDatabase[stateKey] = fSMState;
    }

    public override void RemoveState(Key stateKey)
    {
        this.StatesDatabase.Remove(stateKey);
    }

    public override State GetStateByType(Key type)
    {
        this.StatesDatabase.TryGetValue(type, out State stateToReturn);
        return stateToReturn;
    }

    public override Key GetTypeByState(State stateType)
    {
        foreach (KeyValuePair<Key, State> state in this.StatesDatabase)
        {
            if (state.Value == stateType)
                return state.Key;
        }

        return default(Key);
    }

    public override bool ContainsState(Key stateKey)
    {
        return this.GetStateByType(stateKey) != null;
    }

    public override List<State> GetAllStates()
    {
        return this.StatesDatabase.Values.ToList();
    }

    public override List<Key> GetAllTypes()
    {
        return this.StatesDatabase.Keys.ToList();
    }
}