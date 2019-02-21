using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameSpawner
{
    public abstract Action OnSpawnerReady { get; set; }
}
