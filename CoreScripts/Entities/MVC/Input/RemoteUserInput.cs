using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RemoteUserInput : NetworkBehaviour, IInputControllable
{
    private EntityInputsManager gameInputsManager;

    public LogicEntity LogicEntity { get; set; }

    public void Start()
    {
        this.gameInputsManager = ManagersService.instace.GetManager<EntityInputsManager>();
        this.gameInputsManager.SubscribeToInput(EntityInputType.Move, Move);
        this.gameInputsManager.SubscribeToInput(EntityInputType.Attack, Attack);
    }

    public void Attack(JSONObject data)
    {
        this.CmdAttack();
    }

    public void Move(JSONObject data)
    {
        this.CmdMove(data[GameInputsManager.X_AXIS_KEY].f, data[GameInputsManager.Z_AXIS_KEY].f);
    }

    [Command]
    public void CmdAttack()
    {
        GameEventSystem.instance.DispatchEvent(new EntityInputSentEvent(ActionRequestType.Attack, this.LogicEntity));
    }

    [Command]
    public void CmdMove(float xAxis, float zAxis)
    {
        JSONObject moveInputsData = new JSONObject();
        moveInputsData.AddField(GameInputsManager.X_AXIS_KEY, xAxis);
        moveInputsData.AddField(GameInputsManager.Z_AXIS_KEY, zAxis);

        GameEventSystem.instance.DispatchEvent(new EntityInputSentEvent(ActionRequestType.Move, this.LogicEntity, moveInputsData));
    }

    public void SetLogic(LogicEntity logicEntity)
    {
        this.LogicEntity = logicEntity;
    }
}