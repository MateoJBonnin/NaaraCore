public class AbstractApplicationManagerContainer : AbstractManagerContainer<ApplicationManager>
{
    public AbstractApplicationManagerContainer(ApplicationManager manager) : base(manager)
    {
    }
}

public class AbstractApplicationManagerContainer<T> : AbstractManagerContainer<T> where T : class, ApplicationManager
{
    protected AbstractApplicationManagerContainer(T manager) : base(manager)
    {
    }
}