using System;
using System.Collections;
using Managers;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MEC;

public class SubManagerSystem<T> where T : SubManager
{
    public Action OnAllInitialSubManagersReady;

    protected HashSet<T> subManagers;

    public SubManagerSystem() : this(new List<T>())
    {
    }

    public SubManagerSystem(List<T> subManagers)
    {
        this.subManagers = new HashSet<T>();
        foreach (var manager in subManagers)
            this.SetSubManager(manager);
    }

    public void OnAllInitSubManagersAdded()
    {
        this.WaitForSubManagersToBeReady();
    }

    public void UpdateSubManagers()
    {
        foreach (T subManager in subManagers)
            subManager.UpdateSubManager();
    }

    public List<T> GetAllSubManagers()
    {
        return this.subManagers.ToList();
    }

    public void GetManagerWhenReady<W>(Action<W> onManagerReadyCallback) where W : T
    {
        //COROUTINES DO NOT START WHEN CALL INSIDE OF AN ANONYMOUS CALL
        //The submanager system does not uses the application coroutine manager because even that manager has to be loaded by this manager.
        Timing.RunCoroutine(this.CheckIfManagerIsReady(onManagerReadyCallback));
    }

    public void SetSubManager(T entityManager)
    {
        this.subManagers.Add(entityManager);
    }

    public W GetManager<W>() where W : T
    {
        W tempManager = default;

        IEnumerable<W> wSubManagerList = this.subManagers.Select(subManager => subManager as W);

        foreach (W manager in wSubManagerList)
            if (manager is W)
            {
                tempManager = manager;
                return tempManager as W;
            }

        return tempManager as W;
    }

    private IEnumerator<float> CheckIfManagerIsReady<W>(Action<W> onManagerReadyCallback) where W : T
    {
        W manager = default;
        while (manager == null)
        {
            manager = this.GetManager<W>();
            if (manager != null)
                onManagerReadyCallback(manager);

            yield return Timing.WaitForOneFrame;
        }
    }

    private void WaitForSubManagersToBeReady()
    {
        GameCoroutineManager.instance.StartCoroutine(this.CheckIfAllSubManagersAreReady());
    }

    private IEnumerator CheckIfAllSubManagersAreReady()
    {
        yield return new WaitUntil(() =>
        {
            return subManagers.All(subManager => subManager.subManagerStateFSM.GetCurrentType == SubManagerReadyStates.Ready);
        });

        this.OnAllInitialSubManagersReady?.Invoke();
    }
}