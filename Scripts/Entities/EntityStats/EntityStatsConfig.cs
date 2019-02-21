using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStatsConfig
{
    public EntityStatsConfig(EPrimaryStats EPrimaryStats, EDerivedStats EDerivedStats, ESubStats ESubStats)
    {
        this.ConfigureEntityStats(EPrimaryStats, EDerivedStats, ESubStats);
    }

    public EntityStatsConfig(JSONObject statsData)
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

    public void ConfigureEntityStats(JSONObject statsData)
    {
        if (statsData.HasField(EntityStats.PRIMARY_STATS))
            this.EPrimaryStats = new EPrimaryStats(statsData[EntityStats.PRIMARY_STATS]);
        if (statsData.HasField(EntityStats.DERIVED_STATS))
            this.EDerivedStats = new EDerivedStats(statsData[EntityStats.DERIVED_STATS]);
        if (statsData.HasField(EntityStats.SUB_STATS))
            this.ESubStats = new ESubStats(statsData[EntityStats.SUB_STATS]);
    }

    public EPrimaryStats EPrimaryStats { get; set; }
    public EDerivedStats EDerivedStats { get; set; }
    public ESubStats ESubStats { get; set; }
}
