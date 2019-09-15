public class EntityStatsConfig
{
    public EntityStatsConfig(EPrimaryStats EPrimaryStats, EDerivedStats EDerivedStats, ESubStats ESubStats)
    {
        this.ConfigureEntityStats(EPrimaryStats, EDerivedStats, ESubStats);
    }

    public EntityStatsConfig(EntityStatsData statsData)
    {
        this.ConfigureEntityStats(statsData);
    }

    public void ConfigureEntityStats(EPrimaryStats EPrimaryStats)
    {
        this.EPrimaryStats = EPrimaryStats;
    }

    public void ConfigureEntityStats(EDerivedStats EDerivedStats)
    {
        this.EDerivedStats = EDerivedStats;
    }

    public void ConfigureEntityStats(ESubStats ESubStats)
    {
        this.ESubStats = ESubStats;
    }

    public void ConfigureEntityStats(EPrimaryStats EPrimaryStats, EDerivedStats EDerivedStats, ESubStats ESubStats)
    {
        this.EPrimaryStats = EPrimaryStats;
        this.EDerivedStats = EDerivedStats;
        this.ESubStats = ESubStats;
    }

    public void ConfigureEntityStats(EntityStatsData statsData)
    {
        this.EPrimaryStats = new EPrimaryStats(statsData.entityPrimaryStatsData);
        this.EDerivedStats = new EDerivedStats(statsData.entityDerivedStatsData);
        this.ESubStats = new ESubStats(statsData.entitySubStatsData);
    }

    public EPrimaryStats EPrimaryStats { get; set; }
    public EDerivedStats EDerivedStats { get; set; }
    public ESubStats ESubStats { get; set; }
}
