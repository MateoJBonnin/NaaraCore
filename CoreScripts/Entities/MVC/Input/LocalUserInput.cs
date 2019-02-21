using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class LocalUserInput : MonoBehaviour
{
    private EntityInputsManager gameInputsManager;

    public void Start()
    {
        this.gameInputsManager = ManagersService.instace.GetManager<EntityInputsManager>();
        this.gameInputsManager.SubscribeToInput(EntityInputType.Move, Move);
        this.gameInputsManager.SubscribeToInput(EntityInputType.Attack, Attack);
    }

    public void Attack(JSONObject data)
    {
        GameEventSystem.instance.DispatchEvent(new EntityInputSentEvent(ActionRequestType.Attack));
    }

    public void Move(JSONObject data)
    {
        GameEventSystem.instance.DispatchEvent(new EntityInputSentEvent(ActionRequestType.Move, data));
    }
}