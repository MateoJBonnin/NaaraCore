using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public abstract class Manager : MonoBehaviour
    {
        public Action<Manager> OnManagerReady;

        protected SubManagerSystem<SubManager> subManagerSystem;

        private GameplayManagersInitializer initializer;

        public void Setup(GameplayManagersInitializer initializer)
        {
            this.subManagerSystem = new SubManagerSystem<SubManager>();
            this.subManagerSystem.OnAllInitialSubManagersReady += () => this.OnManagerReady?.Invoke(this);

            this.initializer = initializer;
            this.initializer.OnAllManagersReady += this.OnAllManagersReady;
        }

        public abstract void Init();

        protected virtual void OnAllManagersReady(List<Manager> allManagers) { }
    }
}