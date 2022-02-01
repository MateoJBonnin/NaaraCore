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

    public static bool IsCloseTo(this Transform originPoint, Transform targetPoint, float distance)
    {
        Vector3 originPointYX0 = originPoint.position;
        originPointYX0.y = 0f;
        originPointYX0.x = 0f;

        Vector3 targetPointXY0 = targetPoint.position;
        targetPointXY0.y = 0f;
        targetPointXY0.x = 0f;

        return Vector3.Distance(originPointYX0, targetPointXY0) <= distance;
    }
}
