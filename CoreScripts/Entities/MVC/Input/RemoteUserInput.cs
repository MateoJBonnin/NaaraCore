using Managers;

public class RemoteUserInput : AbstractUserInput
{
    // private EntityInputsManager gameInputsManager;

    public RemoteUserInput(AbstractInputController inputController) : base(inputController, null)
    {
        // this.gameInputsManager = ManagersService.instance.GetManager<EntityInputsManager>();
        //this.gameInputsManager.SubscribeToInput(EntityInputType.Move, Move);
        //this.gameInputsManager.SubscribeToInput(EntityInputType.Attack, Attack);
    }

    public void Attack(EntityInputData data)
    {
        this.CmdAttack();
    }

    public void Move(EntityInputData data)
    {
        this.CmdMove(data.xAxis, data.zAxis);
    }

    //[Command]
    public void CmdAttack()
    {
        //GameEventSystem.instance.DispatchEvent(new EntityInputSentEvent(new ACAttack(), this.LogicEntity));
    }

    //[Command]
    public void CmdMove(float xAxis, float zAxis)
    {
        //EntityInputData moveInputsData = new EntityInputData();
        //moveInputsData.AddField(GameInputsManager.X_AXIS_KEY, xAxis);
        //moveInputsData.AddField(GameInputsManager.Z_AXIS_KEY, zAxis);
        //
        //GameEventSystem.instance.DispatchEvent(new EntityInputSentEvent(ActionRequestType.Move, this.LogicEntity, moveInputsData));
    }

    public override void SetLogic(LogicEntity logicEntity)
    {
        base.SetLogic(logicEntity);
    }

    public override void UpdateInput()
    {
    }

    protected override void ProcessEntityInput(EntityInputData data, ActionFSMState state)
    {
    }
}