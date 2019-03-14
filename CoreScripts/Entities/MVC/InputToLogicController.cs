using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputToLogicController : MonoBehaviour, IInputControllable
{
    public LogicEntity LogicEntity { get; set; }

    public void SetLogic(LogicEntity logicEntity)
    {
        this.LogicEntity = logicEntity;
    }

    public void Start()
    {
        GameEventSystem.instance.AddEventListener<EntityInputSentEvent>(ProcessInputEvent);
    }

    public void Update()
    {
        //TODO: change this for a manager of entities
        this.LogicEntity?.Update();
    }

    public void ProcessInputEvent(EntityInputSentEvent entityInputSentEvent)
    {
        if (this.LogicEntity.Equals(entityInputSentEvent.LogicEntity))
            this.LogicEntity.ProcessAction(entityInputSentEvent.ActionType, entityInputSentEvent.ActionData);
    }
}