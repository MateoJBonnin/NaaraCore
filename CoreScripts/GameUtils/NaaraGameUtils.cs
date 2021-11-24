using UnityEngine;

public static class NaaraGameUtils
{
    public static Vector3 GetDirection(Vector3 originPoint, Vector3 destinationPoint)
    {
        return destinationPoint - originPoint;
    }

    public static float GetPercentageValue(float value, float percent)
    {
        return value / 100 * percent;
    }

    public static bool IsCloseTo(Vector3 originPoint, Vector3 targetPoint, float distance)
    {
        return Vector3.Distance(originPoint, targetPoint) <= distance;
    }

    public static bool IsCloseTo(this Transform originPoint, Vector3 targetPoint, float distance)
    {
        return Vector3.Distance(originPoint.position, targetPoint) <= distance;
    }
}
