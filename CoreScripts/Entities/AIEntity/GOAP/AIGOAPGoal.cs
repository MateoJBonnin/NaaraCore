using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGOAPGoal
{
    public AIWorldState AIWorldGoalState
    { get; set; }

    public AIGOAPGoal()
    {
        this.AIWorldGoalState = new AIWorldState();
    }

    public AIGOAPGoal(List<AIWorldStateSymbol> objetives)
    {
        this.AIWorldGoalState = new AIWorldState();
        foreach (var objetive in objetives)
            this.RegisterNewObjetiveToGoalState(objetive);
    }

    public AIGOAPGoal(AIGOAPGoal aiGOAPGoal)
    {
        this.AIWorldGoalState.UpdateState(aiGOAPGoal.AIWorldGoalState);
    }

    public void RegisterNewObjetiveToGoalState(AIWorldStateSymbol aIWorldStateSymbol)
    {
        this.AIWorldGoalState.RegisterSymbol(aIWorldStateSymbol);
    }

    public void RemoveObjetiveFromGoalState(AIWorldStateSymbol aIWorldStateSymbol)
    {
        this.AIWorldGoalState.RemoveSymbol(aIWorldStateSymbol);
    }
}