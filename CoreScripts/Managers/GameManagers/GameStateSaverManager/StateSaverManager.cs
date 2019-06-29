using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateSaverManager<T>
{
    public List<T> snapshots;
    public abstract AbstractStateSnapshot GetSnapshotState(T stateSaveable);
}