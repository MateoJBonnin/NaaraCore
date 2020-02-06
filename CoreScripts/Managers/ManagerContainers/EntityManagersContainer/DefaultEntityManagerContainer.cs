public class DefaultEntityManagerContainer : AbstractEntityManagerContainer
{
    public DefaultEntityManagerContainer(EntityManager entityManager) : base(entityManager)
    {
    }

    public DefaultEntityManagerContainer(EntityManager entityManager, LogicEntity logicEntity) : base(entityManager)
    {
        entityManager.LogicEntity = logicEntity;
        this.SetState(ManagerReadyStates.Ready);
    }
}

public class DefaultEntityManagerContainer<T> : AbstractEntityManagerContainer<T> where T : class, EntityManager
{
    public DefaultEntityManagerContainer(T entityManager) : base(entityManager)
    {
    }

    public DefaultEntityManagerContainer(T entityManager, LogicEntity logicEntity) : base(entityManager)
    {
        entityManager.LogicEntity = logicEntity;
        this.SetState(ManagerReadyStates.Ready);
    }
}