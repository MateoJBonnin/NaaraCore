using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESubStats : EntityStats
{
    public const string MOVEMENT_SPEED = "movementSpeed";
    public const string ROTATION_SPEED = "rotationSpeed";

    private float movementSpeed;
    public float MovementSpeed
    {
        get
        {
            return this.movementSpeed;
        }
        set
        {
            if (value <= LOWEST_STAT_VALUE)
                this.movementSpeed = LOWEST_STAT_VALUE;
            else
                this.movementSpeed = value;
        }
    }

    private float rotationSpeed;
    public float RotationSpeed
    {
        get
        {
            return this.rotationSpeed;
        }
        set
        {
            if (value <= LOWEST_STAT_VALUE)
                this.rotationSpeed = LOWEST_STAT_VALUE;
            else
                this.rotationSpeed = value;
        }
    }

    public ESubStats(JSONObject subStats) : this(
        subStats[MOVEMENT_SPEED].f,
        subStats[ROTATION_SPEED].f)
    {

    }

    public ESubStats(float movementSpeed, float rotationSpeed)
    {
        this.MovementSpeed = movementSpeed;
        this.RotationSpeed = rotationSpeed;
    }
}
