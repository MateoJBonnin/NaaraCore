public class InputToLogicController : AbstractInputController
{
    public override void SetLogicToControl(LogicEntity logicEntity)
    {
        base.SetLogicToControl(logicEntity);
        this.EnableController();
    }

    public override void EnableController()
    {
        this.LogicEntity.EntityBlackboard.gameEventSystem.AddEventListener<EntityInputSentEvent>(this.ProcessInputEvent);
    }

    public override void DisableController()
    {
        this.LogicEntity.EntityBlackboard.gameEventSystem.RemoveEventListener<EntityInputSentEvent>(this.ProcessInputEvent);
    }

    public void ProcessInputEvent(EntityInputSentEvent entityInputSentEvent)
    {
        if (this.LogicEntity.Equals(entityInputSentEvent.LogicEntity))
            this.LogicEntity.EntityBlackboard.subManagerSystem.GetManager<EntityActionProcessorManager>().ProcessAction(new SimpleEntityActionProcess(entityInputSentEvent.ActionType, new EntityFSMStateData(entityInputSentEvent.ActionData)));
    }
}