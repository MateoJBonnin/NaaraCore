using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewOni : AbstractViewEntity
{
    private const string WALK_ANIMATION_KEY = "Walk";
    private const string IDLE_ANIMATION_KEY = "Idle";

    [SerializeField]
    private Animator animator;
    private Dictionary<ActionRequestType, Action> viewProcessData;
    private string currentTrigger;

    public void Awake()
    {
        this.viewProcessData = new Dictionary<ActionRequestType, Action>();
        this.viewProcessData[ActionRequestType.Move] = Move;
        this.viewProcessData[ActionRequestType.GoTo] = Move;
        this.viewProcessData[ActionRequestType.Idle] = Idle;
    }

    public override void ProcessEntityAction(ActionRequestType actionRequestType)
    {
        this.viewProcessData.TryGetValue(actionRequestType, out Action action);
        action?.Invoke();
    }

    private void Move()
    {
        this.ChangeAnimatorTrigger(WALK_ANIMATION_KEY);
    }

    private void Idle()
    {
        this.ChangeAnimatorTrigger(IDLE_ANIMATION_KEY);
    }

    private void ChangeAnimatorTrigger(string trigger)
    {
        if (this.currentTrigger == trigger)
            return;

        foreach (AnimatorControllerParameter param in this.animator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Trigger)
                this.animator.ResetTrigger(param.name);
        }

        this.currentTrigger = trigger;
        this.animator.SetTrigger(currentTrigger);
    }
}