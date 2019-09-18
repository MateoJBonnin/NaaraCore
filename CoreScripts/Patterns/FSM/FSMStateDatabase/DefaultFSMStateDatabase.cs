using System.Collections.Generic;
using System.Linq;

public class DefaultFSMStateDatabase<T, W> : AbstractFSMStateDatabase<T, W> where W : AbstractFSMData
{
    protected Dictionary<T, FSMState<W>> StatesDatabase { get; private set; }

    public DefaultFSMStateDatabase()
    {
        this.StatesDatabase = new Dictionary<T, FSMState<W>>();
    }

    public DefaultFSMStateDatabase(Dictionary<T, FSMState<W>> statesDatabase) : this()
    {
        this.ConfigureStates(statesDatabase);
    }

    public override void ConfigureStates(Dictionary<T, FSMState<W>> statesData)
    {
        foreach (KeyValuePair<T, FSMState<W>> state in statesData)
            this.SetState(state.Key, state.Value);
    }

    public override void SetState(T stateKey, FSMState<W> fSMState)
    {
        this.StatesDatabase[stateKey] = fSMState;
    }

    public override void RemoveState(T stateKey)
    {
        this.StatesDatabase.Remove(stateKey);
    }

    public override FSMState<W> GetStateByType(T type)
    {
        FSMState<W> stateToReturn;
        this.StatesDatabase.TryGetValue(type, out stateToReturn);
        return stateToReturn;
    }

    public override T GetTypeByState(FSMState<W> stateType)
    {
        foreach (KeyValuePair<T, FSMState<W>> state in this.StatesDatabase)
        {
            if (state.Value == stateType)
                return state.Key;
        }

        return default(T);
    }

    public override List<FSMState<W>> GetAllStates()
    {
        return this.StatesDatabase.Values.ToList();
    }

    public override bool ContainsState(T stateKey)
    {
        return this.GetStateByType(stateKey) != null;
    }
}