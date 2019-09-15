using System;

namespace Managers
{
    public interface IManager
    {
        //event Action<IManager> OnManagerReady;
        void Setup();
        void OnReady();
        void UpdateManager();
    }
}