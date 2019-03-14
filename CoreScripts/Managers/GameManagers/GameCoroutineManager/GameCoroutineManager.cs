using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class GameCoroutineManager : Manager
    {
        public static GameCoroutineManager instance;

        public void Awake()
        {
            instance = this;
        }

        public void Start()
        {
            this.OnManagerReady?.Invoke(this);
        }

        public override void Init()
        {
        }
    }
}