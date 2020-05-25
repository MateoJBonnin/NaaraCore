using System.Collections.Generic;

public class LogicEntityFactory
{
    public LogicEntity GetLogicEntity(List<AbstractLogicEntityManagerContainer> entityManagers, GameplayController gameplayController, ApplicationController applicationController)
    {
        return this.CreateLogicEntity(entityManagers, gameplayController, applicationController);
    }

    private LogicEntity CreateLogicEntity(List<AbstractLogicEntityManagerContainer> entityManagers, GameplayController gameplayController, ApplicationController applicationController)
    {
        LogicEntity logicEntity = new LogicEntity(new LogicEntityBlackboard(applicationController, gameplayController));
        SubManagerSystem<AbstractLogicEntityManagerContainer, LogicEntityManager> entitySubManagerSystem = logicEntity.EntityBlackboard.subManagerSystem;

        for (int i = entityManagers.Count - 1; i >= 0; i--)
        {
            entitySubManagerSystem.RegisterSubManager(entityManagers[i]);
            entityManagers[i].Manager.LogicEntity = logicEntity;
            entityManagers[i].Manager.ApplicationController = applicationController;
            entityManagers[i].Manager.GameplayController = gameplayController;
        }

        List<LogicEntityManager> allEntityManagers = entitySubManagerSystem.GetAllSubManagers();
        entitySubManagerSystem.SetReadyAllManagers();
        entitySubManagerSystem.InitAllManagers();

        for (int i = allEntityManagers.Count - 1; i >= 0; i--)
            allEntityManagers[i].LogicEntity = logicEntity;


        return logicEntity;
    }
}