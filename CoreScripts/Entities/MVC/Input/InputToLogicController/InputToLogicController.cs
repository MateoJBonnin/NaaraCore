public class InputToLogicController : AbstractInputController
{
    public InputToLogicController()
    {
        //TODO: MAYBE TEMPORAL
        this.EnableController();
    }

    public override void EnableController()
    {
        GameEventSystem.instance.AddEventListener<EntityInputSentEvent>(this.ProcessInputEvent);
    }

    public override void DisableController()
    {
        GameEventSystem.instance.RemoveEventListener<EntityInputSentEvent>(this.ProcessInputEvent);
    }

    public void ProcessInputEvent(EntityInputSentEvent entityInputSentEvent)
    {
        if (this.LogicEntity.Equals(entityInputSentEvent.LogicEntity))
            this.LogicEntity.EntityBlackboard.subManagerSystem.GetManager<EntityActionProcessorManager>().ProcessAction(new SimpleEntityActionProcess(null, new EntityFSMStateData(entityInputSentEvent.ActionData)));
    }
}