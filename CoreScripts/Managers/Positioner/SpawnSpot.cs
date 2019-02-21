using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnSpot
{
    private Func<bool> isAvailableFunc;

    public Vector3 SpawnPosition
    {
        get;
        set;
    }

    public SpawnSpot(Func<bool> availableMethod)
    {
        this.isAvailableFunc = availableMethod;
    }

    public SpawnSpot(Vector3 spawnPosition, Func<bool> availableMethod)
    {
        this.SpawnPosition = spawnPosition;
        this.isAvailableFunc = availableMethod;
    }

    public bool isAvailable()
    {
        return this.isAvailableFunc();
    }
}