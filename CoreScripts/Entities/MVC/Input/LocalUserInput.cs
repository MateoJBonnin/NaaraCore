using Managers;

public class LocalUserInput : AbstractInputEntity
{
    private EntityInputsManager gameInputsManager;

    public LocalUserInput(AbstractInputController inputController) : base(inputController)
    {
        this.gameInputsManager = ManagersService.instance.GetManager<EntityInputsManager>();
        this.gameInputsManager.SubscribeToInput(EntityInputType.Move, Move);
        this.gameInputsManager.SubscribeToInput(EntityInputType.Attack, Attack);
    }

    public override void SetLogic(LogicEntity logicEntity)
    {
        base.SetLogic(logicEntity);
        this.LogicEntity = logicEntity;
    }

    public override AbstractInputEntityStateSnapshot TempGatherState()
    {
        return new LocalUserInputEntityStateSnapshot() { inputController = this.inputController };
    }

    public void Attack(EntityInputData data)
    {
        // GameEventSystem.instance.DispatchEvent(new EntityInputSentEvent(ActionRequestType.Attack, this.LogicEntity));
    }

    public void Move(EntityInputData data)
    {
        // GameEventSystem.instance.DispatchEvent(new EntityInputSentEvent(ActionRequestType.Move, this.LogicEntity, data));
    }

    public override void UpdateInput()
    {
    }
}