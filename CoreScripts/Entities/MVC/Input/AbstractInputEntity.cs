public abstract class AbstractInputEntity
{
    public LogicEntity LogicEntity { get; set; }

    protected AbstractInputController inputController;

    protected AbstractInputEntity()
    {
    }

    public AbstractInputEntity(AbstractInputController inputController)
    {
        this.inputController = inputController;
    }

    public virtual void SetLogic(LogicEntity logicEntity)
    {
        this.LogicEntity = logicEntity;
        this.inputController.SetLogicToControl(logicEntity);
    }

    //TEMP, FIX ASAP ALSO THINK TO MAKE THE INPUT ENTITIES FROM HIERARCHY TO COMPONENTS, ALSO MAKE IT NON MONOBEHAVIOUR SINCE
    // WE HAVE INPUT SERVICE
    public abstract AbstractInputEntityStateSnapshot TempGatherState();
    public abstract void UpdateInput();
}