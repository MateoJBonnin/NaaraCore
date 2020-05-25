public class LogicEntity
{
    public LogicEntityBlackboard EntityBlackboard { get; set; }

    //TODO: ENTITY STATS SHOULD BE A SCRIPTABLE OBJECT AND IT SHOULD BE ON ENTITYTSTATEMANAGER
    public LogicEntity(LogicEntityBlackboard entityBlackboard)
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