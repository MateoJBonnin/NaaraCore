using System.Collections.Generic;

public abstract class AbstractFSMStateDatabase<T, W> where W : AbstractFSMData
{
    public abstract void ConfigureStates(Dictionary<T, FSMState<W>> statesData);
    public abstract bool ContainsState(T stateKey);
    public abstract void SetState(T stateKey, FSMState<W> fSMState);
    public abstract void RemoveState(T stateKey);
    public abstract T GetTypeByState(FSMState<W> state);
    public abstract FSMState<W> GetStateByType(T type);
    public abstract List<FSMState<W>> GetAllStates();
}