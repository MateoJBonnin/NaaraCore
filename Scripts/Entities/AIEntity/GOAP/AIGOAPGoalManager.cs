using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGOAPGoalManager
{
    private AbstractAIGOAPGoalThinker goalThinker;

    public AIGOAPGoalManager(AbstractAIGOAPGoalThinker goalThinker)
    {
        this.goalThinker = goalThinker;
    }

    public AIGOAPGoal ProcessGoapGoal()
    {
        return this.goalThinker.ThinkGOAPGoal();
    }
}