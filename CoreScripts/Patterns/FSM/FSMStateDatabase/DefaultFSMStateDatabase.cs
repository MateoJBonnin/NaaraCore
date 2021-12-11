using System.Collections.Generic;
using System.Linq;

public class DefaultFSMStateDatabase<Key, Data> : AbstractFSMStateDatabase<Key, Data> where Data : AbstractFSMData
{
    protected Dictionary<Key, FSMState<Data>> StatesDatabase
    {
        get;
    }

    public DefaultFSMStateDatabase()
    {
        this.StatesDatabase = new Dictionary<Key, FSMState<Data>>();
    }

    public DefaultFSMStateDatabase(Dictionary<Key, FSMState<Data>> statesDatabase) : this()
    {
        this.ConfigureStates(statesDatabase);
    }

    public override void ConfigureStates(Dictionary<Key, FSMState<Data>> statesData)
    {
        foreach (KeyValuePair<Key, FSMState<Data>> state in statesData)
        {
            this.SetState(state.Key, state.Value);
        }
    }

    public override void SetState(Key stateKey, FSMState<Data> fSMState)
    {
        this.StatesDatabase[stateKey] = fSMState;
    }

    public override void RemoveState(Key stateKey)
    {
        this.StatesDatabase.Remove(stateKey);
    }

    public override FSMState<Data> GetStateByType(Key type)
    {
        this.StatesDatabase.TryGetValue(type, out FSMState<Data> stateToReturn);
        return stateToReturn;
    }

    public override Key GetTypeByState(FSMState<Data> stateType)
    {
        foreach (KeyValuePair<Key, FSMState<Data>> state in this.StatesDatabase)
        {
            if (state.Value == stateType)
            {
                return state.Key;
            }
        }

        return default;
    }

    public override bool ContainsState(Key stateKey)
    {
        return this.GetStateByType(stateKey) != null;
    }

    public override List<FSMState<Data>> GetAllStates()
    {
        return this.StatesDatabase.Values.ToList();
    }

    public override List<Key> GetAllTypes()
    {
        return this.StatesDatabase.Keys.ToList();
    }
}

public class DefaultFSMStateDatabaseCustomState<State, Key, Data> : AbstractFSMStateDatabaseCustomState<State, Key, Data> where Data : AbstractFSMData where State : FSMState<Data>
{
    protected Dictionary<Key, State> StatesDatabase
    {
        get;
    }

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
        {
            this.SetState(state.Key, state.Value);
        }
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
            {
                return state.Key;
            }
        }

        return default;
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
