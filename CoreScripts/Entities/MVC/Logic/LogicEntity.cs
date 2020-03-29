public class LogicEntity
{
    public EntityBlackboard EntityBlackboard { get; set; }

    //TODO: ENTITY STATS SHOULD BE A SCRIPTABLE OBJECT AND IT SHOULD BE ON ENTITYTSTATEMANAGER
    public LogicEntity(EntityBlackboard entityBlackboard)
    {
        this.EntityBlackboard = entityBlackboard;
    }

    public void Init()
    {
    }

    public void Update()
    {
        this.EntityBlackboard.UpdateBackboard();
    }
}