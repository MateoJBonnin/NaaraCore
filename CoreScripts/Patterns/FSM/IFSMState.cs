public interface IFSMState<T> where T : AbstractFSMData
{
    public IFSMState<T> SourceState
    {
        get;
        set;
    }

    public void Feed(T data);
    public void OnEnter();
    public void OnUpdate();
    public void OnExit();
}
