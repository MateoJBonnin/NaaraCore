public class ESubStats : EntityStats
{
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

    public ESubStats(EntitySubStatsData subStats)
    {
        this.MovementSpeed = subStats.movementSpeed;
        this.RotationSpeed = subStats.rotationSpeed;
    }

    public ESubStats(float movementSpeed, float rotationSpeed)
    {
        this.MovementSpeed = movementSpeed;
        this.RotationSpeed = rotationSpeed;
    }
}
