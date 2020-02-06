public class AutoInitApplicationManagerContainer : AbstractApplicationManagerContainer
{
    public AutoInitApplicationManagerContainer(ApplicationManager manager) : base(manager)
    {
        this.SetState(ManagerReadyStates.Ready);
    }
}

public class AutoInitApplicationManagerContainer<T> : AbstractApplicationManagerContainer<T> where T : class, ApplicationManager
{
    public AutoInitApplicationManagerContainer(T applicationManager) : base(applicationManager)
    {
        this.SetState(ManagerReadyStates.Ready);
    }
}