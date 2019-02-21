using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class AbstractInputManager<T> : Manager
    {
        public Dictionary<T, List<Action<JSONObject>>> inputsDatabase { get; set; }
        private List<Action<JSONObject>> emptyJsonList = new List<Action<JSONObject>>();

        public AbstractInputManager()
        {
            this.inputsDatabase = new Dictionary<T, List<Action<JSONObject>>>();
        }

        public void SubscribeToInput(T inputType, Action<JSONObject> inputAction)
        {
            List<Action<JSONObject>> actionList = null;
            this.inputsDatabase.TryGetValue(inputType, out actionList);

            if (null == actionList)
                this.inputsDatabase[inputType] = new List<Action<JSONObject>>() { inputAction };
            else
                actionList.Add(inputAction);
        }

        public void RemoveSubscription(T inputType, Action<JSONObject> inputAction)
        {
            List<Action<JSONObject>> actionList = null;
            this.inputsDatabase.TryGetValue(inputType, out actionList);

            if (null != actionList)
                actionList.Remove(inputAction);
        }

        public void TriggerInput(T inputType, JSONObject data)
        {
            List<Action<JSONObject>> actionList = null;
            this.inputsDatabase.TryGetValue(inputType, out actionList);

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