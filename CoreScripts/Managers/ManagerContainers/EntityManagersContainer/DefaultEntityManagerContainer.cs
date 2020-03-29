public class DefaultEntityManagerContainer : AbstractEntityManagerContainer
{
    public DefaultEntityManagerContainer(EntityManager entityManager) : base(entityManager)
    {
    }
}

public class DefaultEntityManagerContainer<T> : AbstractEntityManagerContainer<T> where T : class, EntityManager
{
    public DefaultEntityManagerContainer(T entityManager) : base(entityManager)
    {
    }
}