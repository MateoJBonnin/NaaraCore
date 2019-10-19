public class EntityInputLink
{
    public AbstractEntityInputTrigger entityInput;
    public ActionFSMState actionFSMState;

    public EntityInputLink(AbstractEntityInputTrigger entityInput, ActionFSMState actionFSMState)
    {
        this.entityInput = entityInput;
        this.actionFSMState = actionFSMState;
    }
}