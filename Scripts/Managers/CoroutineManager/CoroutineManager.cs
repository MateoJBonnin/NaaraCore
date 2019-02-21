using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class CoroutineManager : Manager
    {
        public static CoroutineManager instance;

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