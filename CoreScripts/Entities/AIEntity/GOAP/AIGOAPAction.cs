using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAIGOAPAction : IAIAction
{
    bool RequiredWorldState(AIWorldState aIWorldState);
    AIWorldState ConsequenceWorldState(AIWorldState currentWorldState, AIWorldState goalWorldState);
    float GetActionCost(AIWorldState aIWorldState);
    List<Type> WorkingWorldStateSymbols();
}