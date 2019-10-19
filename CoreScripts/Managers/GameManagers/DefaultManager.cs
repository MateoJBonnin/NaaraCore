using Managers;
using System.Collections.Generic;

public abstract class DefaultManager : Manager
{
    protected EmptySimpleFSM<ManagerReadyStates> subManagerStateFSM;

    protected DefaultManager()
    {
        this.subManagerStateFSM = new EmptySimpleFSM<ManagerReadyStates>(new EmptyFSMStateDatabase<ManagerReadyStates>(this.GetSubManagerReadyDatabase()));
    }

    public ManagerReadyStates GetState
    {
        get
        {
            return this.subManagerStateFSM.GetCurrentType;
        }
    }

    public virtual void OnReady()
    {
    }

    public virtual void Setup()
    {
    }

    public virtual void UpdateManager()
    {
    }

    private Dictionary<ManagerReadyStates, FSMState<EmptyFSMStateData>> GetSubManagerReadyDatabase()
    {
        Dictionary<ManagerReadyStates, FSMState<EmptyFSMStateData>> connections = new Dictionary<ManagerReadyStates, FSMState<EmptyFSMStateData>>();

        SimpleFSMState readyState = new SimpleFSMState();

        readyState.OnEnterAction += () =>
        {
            this.OnReady();
        };

        connections[ManagerReadyStates.NotReady] = new SimpleFSMState();
        connections[ManagerReadyStates.Ready] = readyState;

        return connections;
    }
}


public enum ManagerReadyStates
{
    NotReady,
    Ready,
}