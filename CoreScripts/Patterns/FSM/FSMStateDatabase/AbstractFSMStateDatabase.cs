using System.Collections.Generic;

public abstract class AbstractFSMStateDatabase<Key, Data> : AbstractFSMStateDatabaseCustomState<FSMState<Data>, Key, Data> where Data : AbstractFSMData
{

}

public abstract class AbstractFSMStateDatabaseCustomState<State, Key, Data> where Data : AbstractFSMData where State : FSMState<Data>
{
    public abstract void ConfigureStates(Dictionary<Key, State> statesData);
    public abstract bool ContainsState(Key stateKey);
    public abstract void SetState(Key stateKey, State fSMState);
    public abstract void RemoveState(Key stateKey);
    public abstract Key GetTypeByState(State state);
    public abstract State GetStateByType(Key type);
    public abstract List<State> GetAllStates();
    public abstract List<Key> GetAllTypes();
}