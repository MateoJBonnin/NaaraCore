﻿using UnityEngine;

public class NaaraGameUtils
{
    public static Vector3 GetDirection(Vector3 originPoint, Vector3 destinationPoint)
    {
        return destinationPoint - originPoint;
    }

    public static bool IsCloseTo(Vector3 originPoint, Vector3 targetPoint, float distance)
    {
        return Vector3.Distance(originPoint, targetPoint) <= distance;
    }
}