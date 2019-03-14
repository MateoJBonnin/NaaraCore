using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class AbstractInputManager<T> : Manager
    {
        public DictionaryWithListDatabaseStructure<T, Action<JSONObject>> inputsDatabase;
        private List<Action<JSONObject>> emptyJsonList = new List<Action<JSONObject>>();

        public AbstractInputManager()
        {
            this.inputsDatabase = new DictionaryWithListDatabaseStructure<T, Action<JSONObject>>();
        }

        public void SubscribeToInput(T inputType, Action<JSONObject> inputAction)
        {
            this.inputsDatabase.RegisterData(inputType, inputAction);
        }

        public void RemoveSubscription(T inputType, Action<JSONObject> inputAction)
        {
            this.inputsDatabase.RemoveData(inputType, inputAction);
        }

        public void TriggerInput(T inputType, JSONObject data)
        {
            List<Action<JSONObject>> actionList = this.inputsDatabase.GetData(inputType);
            actionList = actionList != null ? actionList : this.emptyJsonList;
            foreach (var action in actionList)
                action?.Invoke(data);
        }

        public override void Init()
        {
        }

        protected void Start()
        {
            this.OnManagerReady?.Invoke(this);
        }
    }
}