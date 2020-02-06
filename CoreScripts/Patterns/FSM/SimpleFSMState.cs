using System;

public class SimpleFSMState : FSMState<EmptyFSMStateData>
{
    public EmptyFSMStateData backupData;
    public Action OnEnterAction;
    public Action OnUpdateAction;
    public Action OnExitAction;

    public override void Feed(EmptyFSMStateData data)
    {
        this.backupData = data;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        this.OnEnterAction?.Invoke();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        this.OnUpdateAction?.Invoke();
    }

    public override void OnExit()
    {
        base.OnExit();
        this.OnExitAction?.Invoke();
    }
}

public class SimpleFSMState<T> : FSMState<T> where T : AbstractFSMData
{
    public T backupData;
    public Action OnEnterAction;
    public Action OnUpdateAction;
    public Action OnExitAction;

    public override void Feed(T data)
    {
        this.backupData = data;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        this.OnEnterAction?.Invoke();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        this.OnUpdateAction?.Invoke();
    }

    public override void OnExit()
    {
        base.OnExit();
        this.OnExitAction?.Invoke();
    }
}