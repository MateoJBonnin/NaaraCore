namespace Managers
{
    public interface Manager
    {
        ManagerReadyStates GetState { get; }
        void Setup();
        void OnReady();
        void UpdateManager();
    }
}