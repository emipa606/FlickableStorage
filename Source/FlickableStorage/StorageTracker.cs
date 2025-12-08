using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace FlickableStorage;

public class StorageTracker(Map map) : MapComponent(map)
{
    private Dictionary<IHaulDestination, int> haulDestinations = new();
    private List<IHaulDestination> tmpHaulDestinationsKeys;
    private List<int> tmpHaulDestinationValues;


    public override void ExposeData()
    {
        base.ExposeData();

        Scribe_Collections.Look(ref haulDestinations, "StockpileStatuses", LookMode.Reference, LookMode.Value,
            ref tmpHaulDestinationsKeys, ref tmpHaulDestinationValues);

        if (!haulDestinations.Any())
        {
            return;
        }

        foreach (var haulDestination in haulDestinations)
        {
            if (haulDestination.Key == null)
            {
                continue;
            }

            GlobalStorageTracker.Instance.UpdateDestinations(haulDestination.Key, haulDestination.Value);
        }

        Log.Message("[FlickableStorage]: Migrated haul destinations to game component.");
        haulDestinations.Clear();
    }
}