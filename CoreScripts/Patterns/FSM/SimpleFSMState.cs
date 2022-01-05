using System;

public class SimpleFSMState : IFSMState<EmptyFSMStateData>
{
    public EmptyFSMStateData backupData;
    public Action OnEnterAction;
    public Action OnUpdateAction;
    public Action OnExitAction;

    public IFSMState<EmptyFSMStateData> SourceState
    {
        get;
        set;
    }

    public void Feed(EmptyFSMStateData data)
    {
        this.backupData = data;
    }

    public void OnEnter()
    {
        this.OnEnterAction?.Invoke();
    }

    public void OnUpdate()
    {
        this.OnUpdateAction?.Invoke();
    }

    public void OnExit()
    {
        this.OnExitAction?.Invoke();
    }
}

public class SimpleIFSMState<T> : IFSMState<T> where T : AbstractFSMData
{
    public T backupData;
    public Action OnEnterAction;
    public Action OnUpdateAction;
    public Action OnExitAction;

    public IFSMState<T> SourceState
    {
        get;
        set;
    }

    public void Feed(T data)
    {
        this.backupData = data;
    }

    public void OnEnter()
    {
        this.OnEnterAction?.Invoke();
    }

    public void OnUpdate()
    {
        this.OnUpdateAction?.Invoke();
    }

    public void OnExit()
    {
        this.OnExitAction?.Invoke();
    }
}
