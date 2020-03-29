public class DefaultLogicEntityManagerContainer : AbstractLogicEntityManagerContainer
{
    public DefaultLogicEntityManagerContainer(LogicEntityManager entityManager) : base(entityManager)
    {
    }

    public DefaultLogicEntityManagerContainer(LogicEntityManager entityManager, LogicEntity logicEntity) : base(entityManager)
    {
        entityManager.LogicEntity = logicEntity;
        this.SetState(ManagerReadyStates.Ready);
    }
}

public class DefaultLogicEntityManagerContainer<T> : AbstractLogicEntityManagerContainer<T> where T : class, LogicEntityManager
{
    public DefaultLogicEntityManagerContainer(T entityManager) : base(entityManager)
    {
    }

    public DefaultLogicEntityManagerContainer(T entityManager, LogicEntity logicEntity) : base(entityManager)
    {
        entityManager.LogicEntity = logicEntity;
        this.SetState(ManagerReadyStates.Ready);
    }
}