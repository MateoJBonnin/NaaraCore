using Managers;

public class DefaultApplicationManagerContainer : AbstractApplicationManagerContainer
{
    public DefaultApplicationManagerContainer(ApplicationManager manager) : base(manager)
    {
    }
}

public class DefaultApplicationManagerContainer<T> : AbstractApplicationManagerContainer<T> where T : class, ApplicationManager
{
    public DefaultApplicationManagerContainer(T manager) : base(manager)
    {
    }
}