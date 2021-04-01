using System;

[Serializable]
public class EntityStateSnapshot : StateSnapshot
{
    public InputEntityStateSnapshot inputEntityStateSnapshot;
    public LogicEntityStateSnapshot entityLogicSnapshot;
    public ViewEntityStateSnapshot entityViewSnapshot;

    public EntityStateSnapshot()
    {
    }

    public EntityStateSnapshot(GameplayController gameplayController, LogicEntity logicEntity, ViewEntity viewEntity, AbstractInputEntity inputEntity)
    {
        entityLogicSnapshot = new LogicEntityStateSnapshot(logicEntity, gameplayController.gameplayManagers.GetManager<LogicSpawnerManager>());
        entityViewSnapshot = new ViewEntityStateSnapshot(viewEntity, gameplayController.gameplayManagers.GetManager<ViewSpawnerManager>());
        //TEMP FIX ASAP, MAKE THE INPUTS LIKE THE LOGIC ENTITY
        inputEntityStateSnapshot = new InputEntityStateSnapshot(inputEntity, gameplayController.gameplayManagers.GetManager<InputSpawnerManager>());
    }
}