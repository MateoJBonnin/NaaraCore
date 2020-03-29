public abstract class AbstractLogicEntityManagerContainer : AbstractLogicEntityManagerContainer<LogicEntityManager>
{
    protected AbstractLogicEntityManagerContainer(LogicEntityManager entityManager) : base(entityManager)
    {
    }
}

public abstract class AbstractLogicEntityManagerContainer<T> : AbstractManagerContainer<T> where T : class, LogicEntityManager
{
    protected AbstractLogicEntityManagerContainer(T entityManager) : base(entityManager)
    {
    }
}