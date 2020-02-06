using Managers;
using System.Collections.Generic;

public abstract class AbstractManagerContainer<T> where T : Manager
{
    public ManagerReadyStates State { get; private set; }
    public T Manager { get; }

    private EmptySimpleFSM<ManagerReadyStates> subManagerStateFSM;

    protected AbstractManagerContainer(T manager)
    {
        this.Manager = manager;
        this.subManagerStateFSM = new EmptySimpleFSM<ManagerReadyStates>(new EmptyFSMStateDatabase<ManagerReadyStates>(this.GetSubManagerReadyDatabase()));
        this.SetState(ManagerReadyStates.NotReady);
    }

    public virtual void SetState(ManagerReadyStates state)
    {
        this.subManagerStateFSM.Feed(state);
    }

    private Dictionary<ManagerReadyStates, FSMState<EmptyFSMStateData>> GetSubManagerReadyDatabase()
    {
        Dictionary<ManagerReadyStates, FSMState<EmptyFSMStateData>> connections = new Dictionary<ManagerReadyStates, FSMState<EmptyFSMStateData>>();

        SimpleFSMState readyState = new SimpleFSMState();
        SimpleFSMState initedState = new SimpleFSMState();
        SimpleFSMState notReadyState = new SimpleFSMState();

        notReadyState.OnEnterAction += () =>
        {
            this.State = ManagerReadyStates.NotReady;
        };

        readyState.OnEnterAction += () =>
        {
            this.State = ManagerReadyStates.Ready;
            this.Manager.OnReady();
        };

        initedState.OnEnterAction += () =>
        {
            this.State = ManagerReadyStates.Inited;
            this.Manager.OnInit();
        };

        connections[ManagerReadyStates.NotReady] = notReadyState;
        connections[ManagerReadyStates.Inited] = initedState;
        connections[ManagerReadyStates.Ready] = readyState;

        return connections;
    }
}


public enum ManagerReadyStates
{
    NotReady,
    Ready,
    Inited,
}