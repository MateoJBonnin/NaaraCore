using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMState
{
    public FSMState sourceState;
    public virtual void SetData(JSONObject data) { }
    public virtual void OnEnter() { }
    public virtual void OnUpdate() { }
    public virtual void OnExit() { }
}
