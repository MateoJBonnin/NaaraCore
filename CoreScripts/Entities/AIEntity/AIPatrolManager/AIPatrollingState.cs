using Managers;
using MEC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrollingState : FSMState<EmptyFSMStateData>
{
    public Action OnPatrollingInterrumped;

    private SimpleFSM<AIPatrollingStates, EmptyFSMStateData> patrollingFSM;
    private AbstractAIPatrolBehaviour aIPatrolBehaviour;
    private AbstractPatrolTimePolicy patrolTimePolicy;

    public AIPatrollingState(AbstractAIPatrolBehaviour aIPatrolBehaviour, AbstractPatrolTimePolicy patrolTimePolicy)
    {
        this.aIPatrolBehaviour = aIPatrolBehaviour;
        this.patrolTimePolicy = patrolTimePolicy;
        this.patrollingFSM = new SimpleFSM<AIPatrollingStates, EmptyFSMStateData>(this.GetAIPatrollingStatesConfig());
    }

    public override void OnEnter()
    {
        this.patrollingFSM.Feed(AIPatrollingStates.Moving);
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
        this.patrollingFSM.Update();
    }

    private Dictionary<AIPatrollingStates, FSMState<EmptyFSMStateData>> GetAIPatrollingStatesConfig()
    {
        Dictionary<AIPatrollingStates, FSMState<EmptyFSMStateData>> connections = new Dictionary<AIPatrollingStates, FSMState<EmptyFSMStateData>>();
        SimpleFSMState movingState = new SimpleFSMState();
        movingState.OnEnterAction += () =>
        {
            this.aIPatrolBehaviour.OnPatrolInterrumped += () => this.patrollingFSM.Feed(AIPatrollingStates.Interrumped);
            this.aIPatrolBehaviour.OnPatrolSpotReached += () =>
            {
                this.patrollingFSM.Feed(AIPatrollingStates.Waiting);
            };

            this.GetToNextPatrolSpot();
        };
        movingState.OnUpdateAction += () =>
        {
            this.aIPatrolBehaviour.UpdatePatrolBehaviour();
        };

        SimpleFSMState waitingState = new SimpleFSMState();
        waitingState.OnEnterAction += () =>
        {
            ApplicationManager.instance.appSystems.GetManager<ApplicationCoroutineManager>().AppCoroutineStarter(this.WaitForNextPatrolStep());
        };

        SimpleFSMState interrumpedState = new SimpleFSMState();
        interrumpedState.OnEnterAction += () =>
        {
            this.OnPatrollingInterrumped?.Invoke();
        };

        connections[AIPatrollingStates.Waiting] = waitingState;
        connections[AIPatrollingStates.Moving] = movingState;
        connections[AIPatrollingStates.Interrumped] = interrumpedState;

        return connections;
    }

    private void GetToNextPatrolSpot()
    {
        this.aIPatrolBehaviour.RunPatrolAction();
    }

    private IEnumerator<float> WaitForNextPatrolStep()
    {
        yield return Timing.WaitForSeconds(this.patrolTimePolicy.GetPatrolTimer());

        this.patrollingFSM.Feed(AIPatrollingStates.Moving);
    }
}

public enum AIPatrollingStates
{
    Waiting,
    Moving,
    Interrumped,
}