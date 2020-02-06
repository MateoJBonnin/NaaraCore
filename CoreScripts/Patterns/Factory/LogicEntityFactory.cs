using System.Collections.Generic;

public class LogicEntityFactory
{
    public LogicEntity GetLogicEntity(List<AbstractEntityManagerContainer> entityManagers, AbstractViewEntity abstractViewEntity, GameplayController gameplayController, ApplicationController applicationController)
    {
        return this.CreateLogicEntity(abstractViewEntity, entityManagers, gameplayController, applicationController);
    }

    private LogicEntity CreateLogicEntity(AbstractViewEntity abstractViewEntity, List<AbstractEntityManagerContainer> entityManagers, GameplayController gameplayController, ApplicationController applicationController)
    {
        LogicEntity logicEntity = new LogicEntity(abstractViewEntity, new EntityBlackboard(applicationController, gameplayController));
        SubManagerSystem<AbstractEntityManagerContainer, EntityManager> entitySubManagerSystem = logicEntity.EntityBlackboard.subManagerSystem;

        for (int i = entityManagers.Count - 1; i >= 0; i--)
        {
            entitySubManagerSystem.RegisterSubManager(entityManagers[i]);
            entityManagers[i].Manager.LogicEntity = logicEntity;
            entityManagers[i].Manager.ApplicationController = applicationController;
            entityManagers[i].Manager.GameplayController = gameplayController;
        }

        List<EntityManager> allEntityManagers = entitySubManagerSystem.GetAllSubManagers();
        entitySubManagerSystem.SetReadyAllManagers();
        entitySubManagerSystem.InitAllManagers();

        for (int i = allEntityManagers.Count - 1; i >= 0; i--)
            allEntityManagers[i].LogicEntity = logicEntity;


        return logicEntity;
    }
}