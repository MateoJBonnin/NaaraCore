public class EntityStatsData
{
    public EntityPrimaryStatsData entityPrimaryStatsData;
    public EntityDerivedStatsData entityDerivedStatsData;
    public EntitySubStatsData entitySubStatsData;

    public EntityStatsData(EntityPrimaryStatsData entityPrimaryStatsData, EntityDerivedStatsData entityDerivedStatsData, EntitySubStatsData entitySubStatsData)
    {
        this.entityPrimaryStatsData = entityPrimaryStatsData;
        this.entityDerivedStatsData = entityDerivedStatsData;
        this.entitySubStatsData = entitySubStatsData;
    }
}