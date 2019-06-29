using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateSaveable
{
    AbstractStateSnapshot GetStateSnapshot();
}