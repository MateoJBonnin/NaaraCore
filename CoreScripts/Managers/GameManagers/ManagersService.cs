using Managers;
using System.Collections.Generic;

public class ManagersService
{
    //FIX THIS ASSAP
    public static ManagersService instance;

    private List<IManager> managers;

    public ManagersService()
    {
        this.managers = new List<IManager>();
        instance = this;
    }

    public ManagersService(List<IManager> managers)
    {
        this.managers = managers;
        instance = this;
    }

    public void SubscribeManager(IManager manager)
    {
        this.managers.Add(manager);
    }

    public void RemoveManager(IManager manager)
    {
        this.managers.Remove(manager);
    }

    public void UpdateManagers()
    {
        for (int i = this.managers.Count - 1; i >= 0; i--)
            managers[i].UpdateManager();
    }

    public T GetManager<T>() where T : class, IManager
    {
        IManager tempManager = null;
        foreach (IManager manager in this.managers)
            if (manager is T)
            {
                tempManager = manager;
                break;
            }

        return tempManager as T;
    }

    public List<IManager> GetAllManagers()
    {
        return this.managers;
    }
}