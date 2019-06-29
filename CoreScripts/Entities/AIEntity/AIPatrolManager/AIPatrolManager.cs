using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolManager : SubManager
{
    public Action OnPatrolInterrumped;

    private AIBlackboard aIBlackboard;
    private SimpleFSM<AIPatrolState> aiPatrolFSM;
    private MEntityPatrolPositionsTracker patrolMemoryTracker;

    public AIPatrolManager(AIBlackboard aIBlackboard, AIBlackboardSetup aIBlackboardSetup)
    {
        this.aIBlackboard = aIBlackboard;
        this.aiPatrolFSM = new SimpleFSM<AIPatrolState>(this.GetAIPatrollingStatesConfig());

        patrolMemoryTracker = new MEntityPatrolPositionsTracker(this.aIBlackboard.AILogicEntity);
        for (int i = aIBlackboardSetup.patrolSetup.AIPatrolPositions.Count - 1; i >= 0; i--)
            patrolMemoryTracker.RegisterPatrolPosition(new EPatrolPositionTracked(aIBlackboardSetup.patrolSetup.AIPatrolPositions[i], this.aIBlackboard.AILogicEntity));

        this.aIBlackboard.EntityBlackboard.subManagerSystem.GetManagerWhenReady<EntityMemoryManager>((entityMemoryManager) => entityMemoryManager.memoryTrackers.SetSubManager(patrolMemoryTracker));
    }

    public void AddPatrolPosition(AIPatrolPosition aIPatrolPosition)
    {
        this.patrolMemoryTracker.RegisterPatrolPosition(new EPatrolPositionTracked(aIPatrolPosition, this.aIBlackboard.AILogicEntity));
    }

    public void RemovePatrolPosition(AIPatrolPosition aIPatrolPosition)
    {
        this.patrolMemoryTracker.UnregisterPatrolPosition(aIPatrolPosition);
    }

    public void Patrol()
    {
        this.aiPatrolFSM.Feed(AIPatrolState.Patrolling);
    }

    public override void UpdateSubManager()
    {
        base.UpdateSubManager();
        this.aiPatrolFSM.Update();
    }

    private Dictionary<AIPatrolState, FSMState> GetAIPatrollingStatesConfig()
    {
        Dictionary<AIPatrolState, FSMState> connections = new Dictionary<AIPatrolState, FSMState>();

        AIPatrollingState patrollingState = new AIPatrollingState(this.aIBlackboard.AIBlackboardSetup.patrolSetup.aIPatrolBehaviour, this.aIBlackboard.AIBlackboardSetup.patrolSetup.patrolTimePolicy);
        patrollingState.OnPatrollingInterrumped += () => this.aiPatrolFSM.Feed(AIPatrolState.Interrumped);

        SimpleFSMState interrumpedState = new SimpleFSMState();
        interrumpedState.OnEnterAction += () =>
        {
            patrollingState.OnPatrollingInterrumped -= () => this.aiPatrolFSM.Feed(AIPatrolState.Interrumped);
            this.OnPatrolInterrumped?.Invoke();
        };

        connections[AIPatrolState.Idle] = new SimpleFSMState();
        connections[AIPatrolState.Patrolling] = patrollingState;
        connections[AIPatrolState.Interrumped] = interrumpedState;

        return connections;
    }
}

public enum AIPatrolState
{
    Idle,
    Patrolling,
    Interrumped,
}