using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAIGoalDecisionTree
{
    public abstract GameDecisionTree<AIGOAPGoal> GetAIGOAPGoal(LogicEntity logicEntity);
}
