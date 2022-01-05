public class EmptyIfsmState : IFSMState<EmptyFSMStateData>
{
    public IFSMState<EmptyFSMStateData> SourceState
    {
        get;
        set;
    }

    public void Feed(EmptyFSMStateData data)
    {
    }

    public void OnEnter()
    {
    }

    public void OnUpdate()
    {
    }

    public void OnExit()
    {
    }
}
