using System;
using System.Collections.Generic;

public abstract class AbstractAIPlanResolver
{
    protected Dictionary<AIPlanState, Func<FSMState<EmptyFSMStateData>>> entityDefaultTypeToAction;
    protected EmptySimpleFSM<AIPlanState> aiPlanFSM { get; set; }
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
        EmptyFSMStateDatabase<AIPlanState> aiPlanDatabase = new EmptyFSMStateDatabase<AIPlanState>(this.GetAIPlanStates());
        this.aiPlanFSM = new EmptySimpleFSM<AIPlanState>(aiPlanDatabase);
        this.aiPlanFSM.Feed(AIPlanState.Idle);
    }

    public abstract void ExecutePlan(List<IAIAction> actions);
    public abstract void UpdatePlan();

    private void SetConfigConnectionStates()
    {
        this.entityDefaultTypeToAction = new Dictionary<AIPlanState, Func<FSMState<EmptyFSMStateData>>>();
        this.entityDefaultTypeToAction[AIPlanState.Idle] = () => this.idleState;
        this.entityDefaultTypeToAction[AIPlanState.StepInterrupted] = () => this.stepInterruptedState;
        this.entityDefaultTypeToAction[AIPlanState.AddStepAfter] = () => this.addStepAfterState;
        this.entityDefaultTypeToAction[AIPlanState.AddStepBefore] = () => this.addStepBeforeState;
        this.entityDefaultTypeToAction[AIPlanState.NextStep] = () => this.nextStepState;
        this.entityDefaultTypeToAction[AIPlanState.Finish] = () => this.finishState;
    }

    private Dictionary<AIPlanState, FSMState<EmptyFSMStateData>> GetAIPlanStates()
    {
        Dictionary<AIPlanState, FSMState<EmptyFSMStateData>> entityActions = new Dictionary<AIPlanState, FSMState<EmptyFSMStateData>>();

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