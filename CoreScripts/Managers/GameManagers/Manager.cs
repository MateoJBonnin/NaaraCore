using System;
using UnityEngine;

namespace Managers
{
    public abstract class Manager : MonoBehaviour, IManagerWSubManagerSystem<SubManager>
    {
        //public abstract event Action<IManager> OnManagerReady;

        public SubManagerSystem<SubManager> SubManagerSystem { get; set; }

        public virtual void UpdateManager()
        {
            this.SubManagerSystem = new SubManagerSystem<SubManager>();
            this.SubManagerSystem.UpdateSubManagers();
        }

        public virtual void Setup()
        {
            //this.SubManagerSystem.OnAllInitialSubManagersReady += () => this.OnManagerReady?.Invoke(this);
        }

        public abstract void OnReady();

        //private void Update()
        //{
        //    this.UpdateManager();
        //}
    }
}