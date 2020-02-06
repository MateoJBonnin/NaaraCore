public class FSMState<T> where T : AbstractFSMData
{
    public FSMState<T> sourceState;
    public virtual void Feed(T data) { }
    public virtual void OnEnter() { }
    public virtual void OnUpdate() { }
    public virtual void OnExit() { }
}