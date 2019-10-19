using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolManager : AIManager
{
    public Action OnPatrolInterrumped;

    private EmptySimpleFSM<AIPatrolState> aiPatrolFSM;
    private MEntityPatrolPositionsTracker patrolMemoryTracker;

    public AIPatrolManager()
    {
        this.aiPatrolFSM = new EmptySimpleFSM<AIPatrolState>(new EmptyFSMStateDatabase<AIPatrolState>(this.GetAIPatrollingStatesConfig()));
        this.patrolMemoryTracker = new MEntityPatrolPositionsTracker(this.aiInput.AILogicEntity);

        //TODO: FIX THIS
        //for (int i = aIBlackboardSetup.patrolSetup.AIPatrolPositions.Count - 1; i >= 0; i--)
        //    this.patrolMemoryTracker.RegisterPatrolPosition(new EPatrolPositionTracked(aIBlackboardSetup.patrolSetup.AIPatrolPositions[i], this.aiInput.aIBlackboard.AILogicEntity));

        this.aiInput.AILogicEntity.EntityBlackboard.subManagerSystem.GetManagerWhenReady<EntityMemoryManager>((entityMemoryManager) => entityMemoryManager.memoryTrackers.RegisterSubManager(this.patrolMemoryTracker));
    }

    public AIPatrolManager(LocalAIInput aiInput) : base(aiInput)
    {
    }

    public void AddPatrolPosition(AIPatrolPosition aIPatrolPosition)
    {
        this.patrolMemoryTracker.RegisterPatrolPosition(new EPatrolPositionTracked(aIPatrolPosition, this.aiInput.AILogicEntity));
    }

    public void RemovePatrolPosition(AIPatrolPosition aIPatrolPosition)
    {
        this.patrolMemoryTracker.UnregisterPatrolPosition(aIPatrolPosition);
    }

    public void Patrol()
    {
        this.aiPatrolFSM.Feed(AIPatrolState.Patrolling);
    }

    public override void UpdateManager()
    {
        base.UpdateManager();
        this.aiPatrolFSM.Update();
    }

    private Dictionary<AIPatrolState, FSMState<EmptyFSMStateData>> GetAIPatrollingStatesConfig()
    {
        Dictionary<AIPatrolState, FSMState<EmptyFSMStateData>> connections = new Dictionary<AIPatrolState, FSMState<EmptyFSMStateData>>();

        //TODO: FIX THIS MAYBE USE SCRIPTABLE OBJECTS
        AIPatrollingState patrollingState = new AIPatrollingState(null, null);//TODO:FIX THIS this.aiInput.AIBlackboardSetup.patrolSetup.aIPatrolBehaviour, this.aiInput.aIBlackboard.AIBlackboardSetup.patrolSetup.patrolTimePolicy);
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