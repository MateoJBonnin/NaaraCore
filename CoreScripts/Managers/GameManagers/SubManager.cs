using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SubManager
{
    public SimpleFSM<SubManagerReadyStates> subManagerStateFSM;
    public Action OnSubManagerReady { get; set; }

    public SubManager()
    {
        this.subManagerStateFSM = new SimpleFSM<SubManagerReadyStates>(this.GetSubManagerReadyConfig());
        this.OnSubManagerReady += () => this.subManagerStateFSM.Feed(SubManagerReadyStates.Ready);
    }

    public virtual void UpdateSubManager()
    {
    }

    private Dictionary<SubManagerReadyStates, FSMState> GetSubManagerReadyConfig()
    {
        Dictionary<SubManagerReadyStates, FSMState> connections = new Dictionary<SubManagerReadyStates, FSMState>();

        connections[SubManagerReadyStates.NotReady] = new SimpleFSMState();
        connections[SubManagerReadyStates.Ready] = new SimpleFSMState();

        return connections;
    }
}


public enum SubManagerReadyStates
{
    NotReady,
    Ready,
}