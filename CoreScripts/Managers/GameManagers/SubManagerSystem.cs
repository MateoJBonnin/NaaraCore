using System;
using System.Collections;
using Managers;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        GameCoroutineManager.instance.StartCoroutine(this.CheckIfManagerIsReady(onManagerReadyCallback));
    }

    public W GetManagerWhenReady<W>() where W : T
    {
        W tempManager = default;
        this.GetManagerWhenReady<W>((manager) => tempManager = manager);
        return tempManager;
    }

    public void SetSubManager(T entityManager)
    {
        this.subManagers.Add(entityManager);
    }

    private IEnumerator CheckIfManagerIsReady<W>(Action<W> onManagerReadyCallback) where W : T
    {
        W manager = default;
        yield return new WaitUntil(() =>
        {
            manager = this.GetManager<W>();

            bool predicate = manager != null;

            if (predicate)
                onManagerReadyCallback(manager);

            return predicate;
        });

    }

    private W GetManager<W>() where W : T
    {
        W tempManager = default;
        IEnumerable<W> wSubManagerList = this.subManagers.Select(subManager => subManager as W);

        foreach (W manager in wSubManagerList)
            if (manager is W)
                tempManager = manager;

        return tempManager as W;
    }

    private void WaitForSubManagersToBeReady()
    {
        GameCoroutineManager.instance.StartCoroutine(this.CheckIfAllSubManagersAreReady());
    }

    private IEnumerator CheckIfAllSubManagersAreReady()
    {
        yield return new WaitUntil(() =>
        {
            return subManagers.All(subManager => subManager.subManagerStateFSM.GetCurrentType() == SubManagerReadyStates.Ready);
        });

        this.OnAllInitialSubManagersReady?.Invoke();
    }
}