using Managers;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SubManagerSystem<ManagerContainer, ManagerType> where ManagerType : Manager where ManagerContainer : AbstractManagerContainer<ManagerType>
{
    public event Action OnAllManagersInited;

    protected HashSet<ManagerContainer> subManagers;

    public SubManagerSystem() : this(new List<ManagerContainer>())
    {
    }

    public SubManagerSystem(List<ManagerContainer> subManagers)
    {
        this.subManagers = new HashSet<ManagerContainer>();
        foreach (ManagerContainer manager in subManagers)
            this.RegisterSubManager(manager);
    }

    public void SetReadyAllManagers()
    {
        foreach (var manager in subManagers)
            manager.SetState(ManagerReadyStates.Ready);
    }

    public void InitAllManagers()
    {
        this.WaitAndInitAllSubManagers();
    }

    public void UpdateSubManagers()
    {
        foreach (ManagerType subManager in subManagers.Where(subManager => subManager.State == ManagerReadyStates.Inited).Select(managerContainer => managerContainer.Manager).ToList())
            subManager.UpdateManager();
    }

    public List<ManagerType> GetAllSubManagers()
    {
        return this.subManagers.Select(managerContainer => (ManagerType)managerContainer.Manager).ToList();
    }

    public void GetManagerWhenReady<W>(Action<W> onManagerReadyCallback) where W : class, ManagerType
    {
        //COROUTINES DO NOT START WHEN CALL INSIDE OF AN ANONYMOUS CALL
        //The submanager system does not uses the application coroutine manager because even that manager has to be loaded by this manager.
        Timing.RunCoroutine(this.CheckIfManagerIsReady(onManagerReadyCallback));
    }

    public void RegisterSubManager(ManagerContainer entityManagerContainer)
    {
        this.subManagers.Add(entityManagerContainer);
    }

    public W GetManager<W>() where W : Manager
    {
        W tempManager = default;

        IEnumerable<ManagerType> wSubManagerList = this.subManagers.Select(subManager => subManager.Manager);

        foreach (Manager manager in wSubManagerList)
        {
            if (manager is W)
            {
                tempManager = (W)manager;
                return tempManager;
            }
        }

        return tempManager;
    }

    private IEnumerator<float> CheckIfManagerIsReady<W>(Action<W> onManagerReadyCallback) where W : class, ManagerType
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

    private void WaitAndInitAllSubManagers()
    {
        Timing.RunCoroutine(this.CheckAndInitAllSubManagers(
            () =>
            {
                foreach (var manager in subManagers)
                    manager.SetState(ManagerReadyStates.Inited);

                this.OnAllManagersInited?.Invoke();
            }));
    }

    private IEnumerator<float> CheckAndInitAllSubManagers(Action onAllReady)
    {
        yield return Timing.WaitUntilDone(() =>
        {
            return subManagers.All(subManager => subManager.State == ManagerReadyStates.Ready);
        });

        yield return 0;

        onAllReady?.Invoke();
    }
}