using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFSMState : FSMState
{
    public JSONObject backupData;
    public Action OnEnterAction;
    public Action OnUpdateAction;
    public Action OnExitAction;

    public override void SetData(JSONObject data)
    {
        this.backupData = data;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        this.OnEnterAction?.Invoke();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        this.OnUpdateAction?.Invoke();
    }

    public override void OnExit()
    {
        base.OnExit();
        this.OnExitAction?.Invoke();
    }
}
