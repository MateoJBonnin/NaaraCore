using Managers;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;

public class SubManagerSystem<T> where T : class, Manager
{
    public Action OnAllInitialSubManagersReady;

    protected HashSet<T> subManagers;

    public SubManagerSystem() : this(new List<T>())
    {
    }

    public SubManagerSystem(List<T> subManagers)
    {
        this.subManagers = new HashSet<T>();
        foreach (T manager in subManagers)
            this.RegisterSubManager(manager);

        this.CheckSubManagersState();
    }

    public void InitAllManagers()
    {
        foreach (T manager in subManagers)
            manager.Setup();
    }

    public void UpdateSubManagers()
    {
        foreach (T subManager in subManagers)
            subManager.UpdateManager();
    }

    public List<T> GetAllSubManagers()
    {
        return this.subManagers.ToList();
    }

    public void GetManagerWhenReady<W>(Action<W> onManagerReadyCallback) where W : class, T
    {
        //COROUTINES DO NOT START WHEN CALL INSIDE OF AN ANONYMOUS CALL
        //The submanager system does not uses the application coroutine manager because even that manager has to be loaded by this manager.
        Timing.RunCoroutine(this.CheckIfManagerIsReady(onManagerReadyCallback));
    }

    public void RegisterSubManager(T entityManager)
    {
        this.subManagers.Add(entityManager);
    }

    public W GetManager<W>() where W : class, T
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

    private IEnumerator<float> CheckIfManagerIsReady<W>(Action<W> onManagerReadyCallback) where W : class, T
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

    private void CheckSubManagersState()
    {
        Timing.RunCoroutine(this.CheckIfAllSubManagersAreReady());
    }

    private IEnumerator<float> CheckIfAllSubManagersAreReady()
    {
        yield return Timing.WaitUntilDone(() =>
        {
            return subManagers.All(subManager => subManager.GetState == ManagerReadyStates.Ready);
        });

        this.OnAllInitialSubManagersReady?.Invoke();
    }
}