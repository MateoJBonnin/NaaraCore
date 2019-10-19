using Managers;
using System.Collections.Generic;

public class ManagersService
{
    //FIX THIS ASSAP
    public static ManagersService instance;

    public SubManagerSystem<Manager> managers;

    public ManagersService() : this(new List<Manager>())
    {
    }

    public ManagersService(List<Manager> managers)
    {
        instance = this;
        this.managers = new SubManagerSystem<Manager>();
        for (int i = managers.Count - 1; i >= 0; i--)
            this.managers.RegisterSubManager(managers[i]);
    }

    public void UpdateManagers()
    {
        this.managers.UpdateSubManagers();
    }

    public T GetManager<T>() where T : DefaultManager
    {
        return this.managers.GetManager<T>();
    }

    public void SubscribeManager(Manager manager)
    {
        this.managers.RegisterSubManager(manager);
    }

    public List<Manager> GetAllManagers()
    {
        return this.managers.GetAllSubManagers();
    }
}