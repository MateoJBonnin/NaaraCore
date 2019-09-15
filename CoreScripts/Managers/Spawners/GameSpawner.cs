using System;

public abstract class GameSpawner
{
    public abstract Action OnSpawnerReady { get; set; }
}
