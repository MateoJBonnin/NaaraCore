using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIWorldStateSymbol
{
    public abstract bool IsSatisfied(AIWorldState aIWorldState);
}