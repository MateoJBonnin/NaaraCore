using System.Collections.Generic;
using System.Linq;

public class NoKeyFSMStateDatabase<Data> : AbstractFSMStateDatabase<IFSMState<Data>, Data> where Data : AbstractFSMData
{
    public Dictionary<IFSMState<Data>, IFSMState<Data>> StatesData
    {
        get;
        protected set;
    } = new Dictionary<IFSMState<Data>, IFSMState<Data>>();

    public NoKeyFSMStateDatabase(HashSet<IFSMState<Data>> statesData)
    {
        this.ConfigureStates(statesData);
    }

    protected virtual void ConfigureStates(HashSet<IFSMState<Data>> statesData)
    {
        foreach (IFSMState<Data> state in statesData)
        {
            this.StatesData.Add(state, state);
        }
    }

    public override bool ContainsState(IFSMState<Data> stateKey)
    {
        return this.StatesData.ContainsKey(stateKey);
    }

    public override void SetState(IFSMState<Data> stateKey, IFSMState<Data> FSMState)
    {
        this.StatesData[stateKey] = FSMState;
    }

    public override void RemoveState(IFSMState<Data> stateKey)
    {
        this.StatesData.Remove(stateKey);
    }

    public override IFSMState<Data> GetTypeByState(IFSMState<Data> state)
    {
        return state;
    }

    public override IFSMState<Data> GetStateByType(IFSMState<Data> type)
    {
        return type;
    }

    public override List<IFSMState<Data>> GetAllStates()
    {
        return this.StatesData.Keys.ToList();
    }

    public override List<IFSMState<Data>> GetAllTypes()
    {
        return this.StatesData.Values.ToList();
    }
}

public class NoKeyFSMStateDatabaseCustomState<Data> : NoKeyFSMStateDatabase<Data> where Data : AbstractFSMData
{
    public NoKeyFSMStateDatabaseCustomState(HashSet<IFSMState<Data>> statesData) : base(statesData)
    {
    }
}

public class SimpleNoKeyFSMStateDatabaseCustomState<State, Data> : DefaultFSMStateDatabaseCustomState<State, State, Data> where Data : AbstractFSMData where State : IFSMState<Data>
{
    public SimpleNoKeyFSMStateDatabaseCustomState(Dictionary<State, State> states) : base(states)
    {
    }
}

public class EmptyNoKeyFSMStateDatabase : NoKeyFSMStateDatabase<AbstractFSMData>
{
    public EmptyNoKeyFSMStateDatabase(HashSet<IFSMState<AbstractFSMData>> statesData) : base(statesData)
    {
    }
}


public class EmptyNoKeyFSMStateDatabaseCustomData<Data> : NoKeyFSMStateDatabase<Data> where Data : AbstractFSMData
{
    public EmptyNoKeyFSMStateDatabaseCustomData(HashSet<IFSMState<Data>> statesData) : base(statesData)
    {
    }
}
