using Newtonsoft.Json;

public abstract class AbstractInputController
{
    [JsonIgnore]
    public LogicEntity LogicEntity { get; set; }

    public virtual void SetLogicToControl(LogicEntity logicEntity)
    {
        this.LogicEntity = logicEntity;
    }

    public abstract void EnableController();
    public abstract void DisableController();
}