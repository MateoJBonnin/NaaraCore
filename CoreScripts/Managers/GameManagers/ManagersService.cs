using Managers;
using System.Collections.Generic;
using UnityEngine;

public class ManagersService
{
    public static ManagersService instace;

    [SerializeField]
    private List<Manager> managers;

    public ManagersService()
    {
        if (instace == null)
            instace = this;

        this.managers = new List<Manager>();
    }

    public void SubscribeManager(Manager manager)
    {
        this.managers.Add(manager);
    }

    public void RemoveManager(Manager manager)
    {
        this.managers.Remove(manager);
    }

    public T GetManager<T>() where T : Manager
    {
        Manager tempManager = null;
        foreach (Manager manager in this.managers)
            if (manager is T)
            {
                tempManager = manager;
                break;
            }

        return tempManager as T;
    }
}
