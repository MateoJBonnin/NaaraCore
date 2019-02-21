using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUtils
{
    public static Vector3 GetDirection(Vector3 originPoint, Vector3 destinationPoint)
    {
        return destinationPoint - originPoint;
    }
}
