using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAIPlanResolver
{
    protected Dictionary<AIPlanState, Func<FSMState>> entityDefaultTypeToAction;
    protected GenericFSM<AIPlanState> aiPlanFSM { get; set; }
    protected Action OnActionInterrumped;

    protected SimpleFSMState idleState = new SimpleFSMState();
    protected SimpleFSMState stepInterruptedState = new SimpleFSMState();
    protected SimpleFSMState addStepAfterState = new SimpleFSMState();
    protected SimpleFSMState addStepBeforeState = new SimpleFSMState();
    protected SimpleFSMState nextStepState = new SimpleFSMState();
    protected SimpleFSMState finishState = new SimpleFSMState();
    protected IAIAction currentAction;

    public AbstractAIPlanResolver(Action onActionInterrumped)
    {
        this.OnActionInterrumped = onActionInterrumped;

        this.SetConfigConnectionStates();
        FSMConfig<AIPlanState> aiPlanFSMConfig = new FSMConfig<AIPlanState>(JSONObject.arr, this.GetAIPlanStates());
        this.aiPlanFSM = new GenericFSM<AIPlanState>(aiPlanFSMConfig, new FSMForcedTransitioner<AIPlanState>(aiPlanFSMConfig));
        this.aiPlanFSM.Feed(AIPlanState.Idle);
    }

    public abstract void ExecutePlan(List<IAIAction> actions);
    public abstract void UpdatePlan();

    private void SetConfigConnectionStates()
    {
        this.entityDefaultTypeToAction = new Dictionary<AIPlanState, Func<FSMState>>();
        this.entityDefaultTypeToAction[AIPlanState.Idle] = () => this.idleState;
        this.entityDefaultTypeToAction[AIPlanState.StepInterrupted] = () => this.stepInterruptedState;
        this.entityDefaultTypeToAction[AIPlanState.AddStepAfter] = () => this.addStepAfterState;
        this.entityDefaultTypeToAction[AIPlanState.AddStepBefore] = () => this.addStepBeforeState;
        this.entityDefaultTypeToAction[AIPlanState.NextStep] = () => this.nextStepState;
        this.entityDefaultTypeToAction[AIPlanState.Finish] = () => this.finishState;
    }

    private Dictionary<AIPlanState, FSMState> GetAIPlanStates()
    {
        Dictionary<AIPlanState, FSMState> entityActions = new Dictionary<AIPlanState, FSMState>();

        for (int i = 0, count = (int)AIPlanState.Count; i < count; i++)
            entityActions[(AIPlanState)i] = this.entityDefaultTypeToAction[(AIPlanState)i]();

        return entityActions;
    }
}

public enum AIPlanState
{
    Idle,
    StepInterrupted,
    AddStepAfter,
    AddStepBefore,
    NextStep,
    Finish,
    Count
}