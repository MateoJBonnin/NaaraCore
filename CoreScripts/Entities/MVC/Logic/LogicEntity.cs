public class LogicEntity
{
    public AbstractViewEntity ViewEntity { get; set; }
    public EntityBlackboard EntityBlackboard { get; set; }

    //TODO: ENTITY STATS SHOULD BE A SCRIPTABLE OBJECT AND IT SHOULD BE ON ENTITYTSTATEMANAGER
    public LogicEntity(AbstractViewEntity viewEntity)
    {
        this.Init(viewEntity);
        this.EntityBlackboard = new EntityBlackboard();
    }

    public void Init(AbstractViewEntity viewEntity)
    {
        this.ViewEntity = viewEntity;
    }

    public void Update()
    {
        this.EntityBlackboard.UpdateBackboard();
    }
}