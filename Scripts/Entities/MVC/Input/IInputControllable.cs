using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputControllable
{
    LogicEntity LogicEntity { get; set; }
    void SetLogic(LogicEntity logicEntity);
}
