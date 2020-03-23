using Newtonsoft.Json;

public abstract class AbstractInputController
{
    [JsonIgnore]
    public AbstractViewEntity ViewEntity { get; set; }
    public LogicEntity LogicEntity { get; set; }

    public virtual void SetLogicToControl(LogicEntity logicEntity)
    {
        this.LogicEntity = logicEntity;
    }

    public virtual void SetViewToControl(AbstractViewEntity viewEntity)
    {
        this.ViewEntity = viewEntity;
    }

    public abstract void EnableController();
    public abstract void DisableController();
}