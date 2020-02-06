public abstract class AbstractEntityManagerContainer : AbstractManagerContainer<EntityManager>
{
    protected AbstractEntityManagerContainer(EntityManager entityManager) : base(entityManager)
    {
    }
}

public abstract class AbstractEntityManagerContainer<T> : AbstractManagerContainer<T> where T : class, EntityManager
{
    protected AbstractEntityManagerContainer(T entityManager) : base(entityManager)
    {
    }
}