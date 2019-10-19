using System.Collections.Generic;

public class LogicEntityFactory
{
    public LogicEntity GetLogicEntity(List<EntityManager> entityManagers, AbstractViewEntity abstractViewEntity)
    {
        return this.CreateLogicEntity(abstractViewEntity, entityManagers);
    }

    private LogicEntity CreateLogicEntity(AbstractViewEntity abstractViewEntity, List<EntityManager> entityManagers)
    {
        LogicEntity logicEntity = new LogicEntity(abstractViewEntity);
        SubManagerSystem<EntityManager> entitySubManagerSystem = logicEntity.EntityBlackboard.subManagerSystem;

        for (int i = entityManagers.Count - 1; i >= 0; i--)
            entitySubManagerSystem.RegisterSubManager(entityManagers[i]);

        List<EntityManager> allEntityManagers = entitySubManagerSystem.GetAllSubManagers();
        entitySubManagerSystem.InitAllManagers();

        for (int i = allEntityManagers.Count - 1; i >= 0; i--)
            allEntityManagers[i].LogicEntity = logicEntity;


        return logicEntity;
    }
}