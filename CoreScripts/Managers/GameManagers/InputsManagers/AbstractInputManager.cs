using System;
using System.Collections.Generic;

namespace Managers
{
    public abstract class AbstractInputManager<T, W> : IManager where W : AbstractInputData
    {
        public GenericDatabaseStructure<T, Action<W>> inputsDatabase;
        private List<Action<W>> cachedEmptyActionList = new List<Action<W>>();

        public AbstractInputManager()
        {
            this.inputsDatabase = new GenericDatabaseStructure<T, Action<W>>();
        }

        public void SubscribeToInput(T inputType, Action<W> inputAction)
        {
            this.inputsDatabase.RegisterData(inputType, inputAction);
        }

        public void RemoveSubscription(T inputType, Action<W> inputAction)
        {
            this.inputsDatabase.RemoveData(inputType, inputAction);
        }

        public void TriggerInput(T inputType, W data)
        {
            List<Action<W>> actionList = this.inputsDatabase.GetData(inputType);
            actionList = actionList != null ? actionList : this.cachedEmptyActionList;
            foreach (var action in actionList)
                action?.Invoke(data);
        }

        //protected void Start()
        //{
        //    //this.OnManagerReady?.Invoke(this);
        //}

        public abstract void Setup();
        public abstract void OnReady();
        public abstract void UpdateManager();
    }
}