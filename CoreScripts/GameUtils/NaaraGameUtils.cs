using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaaraGameUtils
{
    public static Vector3 GetDirection(Vector3 originPoint, Vector3 destinationPoint)
    {
        return destinationPoint - originPoint;
    }
}