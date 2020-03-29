public class AbstractLogicEntityManager : AbstractEntityManager, LogicEntityManager
{
    public LogicEntity LogicEntity
    {
        get
        {
            return this.logicEntity;
        }
        set
        {
            this.logicEntity = value;
            this.OnLogicEntityConfigured(logicEntity);

        }
    }

    private LogicEntity logicEntity;

    public virtual void OnLogicEntityConfigured(LogicEntity logicEntity)
    {
    }
}