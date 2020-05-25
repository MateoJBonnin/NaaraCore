public class EntitiesViewStateUpdaterManager : AbstractGameplayManager
{
    private ViewGameEntityManager viewGameEntityManager;

    public override void OnInit()
    {
        base.OnInit();
        this.viewGameEntityManager = this.GameplayController.gameplayManagers.GetManager<GameEntityManager>().GetSubManager<ViewGameEntityManager>();
        this.GameplayController.gameplayManagers.GetManager<GameEventSystemLoader>().gameEventSystem.AddEventListener<GameLogicToViewEntityUpdaterEvent>(this.OnLogicToViewEventHandler);
    }

    private void OnLogicToViewEventHandler(GameLogicToViewEntityUpdaterEvent logicToViewEventUpdater)
    {
        logicToViewEventUpdater.viewEntityAction(viewGameEntityManager.GetEntitiesByIndex(logicToViewEventUpdater.logicIndex));
    }
}