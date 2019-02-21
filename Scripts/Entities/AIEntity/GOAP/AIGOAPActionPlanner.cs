using Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIGOAPActionPlanner
{
    private const float PLAN_FAIL_DEFAULT_TIMER = 300;

    private PathfinderManager<AIWorldState> worldStatePathfinder;
    private float planFailTimer;
    private AIBlackboard blackboard;

    public AIGOAPActionPlanner(AIBlackboard blackboard, float planFailTimer = PLAN_FAIL_DEFAULT_TIMER)
    {
        this.blackboard = blackboard;
        this.worldStatePathfinder = new PathfinderManager<AIWorldState>();
        this.planFailTimer = planFailTimer;
    }

    public System.Tuple<List<AIWorldState>, Dictionary<AIWorldState, AIWorldState>> ResolvePlan(Func<List<IAIGOAPAction>> getActions, Func<AIGOAPGoal> getGoalWorldState)
    {
        AIWorldState goalState = getGoalWorldState().AIWorldGoalState;
        AIWorldState initialGoal = this.GetInitialWorldState();
        List<IAIGOAPAction> entityActions = getActions();
        float watchDog = 0;

        return this.worldStatePathfinder.GetAstarPathfind(
            initialGoal,
            goalState,
            currentState => goalState.IsSatisfiedWith(currentState) || watchDog > this.planFailTimer,
            (currentState, goal) => currentState.stepsCount,
            currentState =>
            {
                watchDog++;
                List<Connection<AIWorldState>> connection = new List<Connection<AIWorldState>>();
                List<AIWorldStateSymbol> notSatisfiedSymbols = goalState.GetNotSatisfiedWorldSymbols(currentState);
                foreach (IAIGOAPAction action in entityActions)
                {
                    bool neededAction = false;
                    foreach (var symbol in notSatisfiedSymbols)
                        neededAction = action.WorkingWorldStateSymbols().Any(s => symbol.GetType().Equals(s));

                    if (action.RequiredWorldState(currentState) && neededAction)
                    {
                        AIWorldState newWorldState = new AIWorldState();
                        newWorldState.stepsCount++;
                        newWorldState.UpdateState(currentState);
                        newWorldState.UpdateState(AIWorldState.CopyWorldState(action.ConsequenceWorldState(newWorldState, goalState)));
                        newWorldState.builderAction = action;
                        connection.Add(new Connection<AIWorldState>(newWorldState, action.GetActionCost(newWorldState)));
                    }
                }
                return connection;
            });
    }

    private AIWorldState GetInitialWorldState()
    {
        AIWorldState aIWorldState = new AIWorldState();
        aIWorldState.RegisterSymbol(new WSSLogicSelf(this.blackboard.AILogicEntity));
        return aIWorldState;
    }
}