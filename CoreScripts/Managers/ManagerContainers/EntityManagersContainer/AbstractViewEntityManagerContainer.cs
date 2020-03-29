public abstract class AbstractViewEntityManagerContainer : AbstractManagerContainer<ViewEntityManager>
{
    protected AbstractViewEntityManagerContainer(ViewEntityManager entityManager) : base(entityManager)
    {
    }
}

public abstract class AbstractViewEntityManagerContainer<T> : AbstractManagerContainer<T> where T : class, ViewEntityManager
{
    protected AbstractViewEntityManagerContainer(T entityManager) : base(entityManager)
    {
    }
}