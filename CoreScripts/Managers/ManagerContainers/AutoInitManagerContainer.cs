using Managers;

public class AutoInitManagerContainer : AbstractManagerContainer<Manager>
{
    public AutoInitManagerContainer(Manager manager) : base(manager)
    {
        this.SetState(ManagerReadyStates.Ready);
    }
}

public class AutoInitManagerContainer<T> : AbstractManagerContainer<T> where T : class, Manager
{
    public AutoInitManagerContainer(T manager) : base(manager)
    {
        this.SetState(ManagerReadyStates.Ready);
    }
}