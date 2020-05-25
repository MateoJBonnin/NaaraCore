public class DefaultViewEntityManagerContainer : AbstractViewEntityManagerContainer
{
    public DefaultViewEntityManagerContainer(ViewEntityManager entityManager) : base(entityManager)
    {
    }

    public DefaultViewEntityManagerContainer(ViewEntityManager entityManager, ViewEntity viewEntity) : base(entityManager)
    {
        entityManager.ViewEntity = viewEntity;
        this.SetState(ManagerReadyStates.Ready);
    }
}

public class DefaultViewEntityManagerContainer<T> : AbstractViewEntityManagerContainer<T> where T : class, ViewEntityManager
{
    public DefaultViewEntityManagerContainer(T entityManager) : base(entityManager)
    {
    }

    public DefaultViewEntityManagerContainer(T entityManager, ViewEntity viewEntity) : base(entityManager)
    {
        entityManager.ViewEntity = viewEntity;
        this.SetState(ManagerReadyStates.Ready);
    }
}