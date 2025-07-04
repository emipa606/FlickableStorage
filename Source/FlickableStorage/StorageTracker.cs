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

    public int this[IHaulDestination destination] => haulDestinations[destination];

    public bool Has(IHaulDestination haulDestination)
    {
        return haulDestinations.ContainsKey(haulDestination);
    }

    public void UpdateDestinations(IHaulDestination destination, int newValue)
    {
        haulDestinations[destination] = newValue;

        if (destination is not IStorageGroupMember storageGroupMember)
        {
            return;
        }

        if (storageGroupMember.Group == null)
        {
            return;
        }

        var otherMembers = storageGroupMember.Group.members.Where(member => member != storageGroupMember).ToArray();
        if (!otherMembers.Any())
        {
            return;
        }

        foreach (var groupMember in otherMembers)
        {
            if (groupMember is not IHaulDestination linkedDestination)
            {
                continue;
            }

            haulDestinations[linkedDestination] = newValue;
        }
    }

    public override void ExposeData()
    {
        base.ExposeData();
        if (Scribe.mode == LoadSaveMode.Saving)
        {
            cull();
        }

        Scribe_Collections.Look(ref haulDestinations, "StockpileStatuses", LookMode.Reference, LookMode.Value,
            ref tmpHaulDestinationsKeys, ref tmpHaulDestinationValues);
    }

    private void cull()
    {
        foreach (var destination in haulDestinations.Keys.ToList())
        {
            if (destination == null)
            {
                continue;
            }

            var inPlace = map.haulDestinationManager.AllHaulDestinations
                .FirstOrDefault(d => d.Position == destination.Position);
            if (inPlace != destination || haulDestinations[destination] == 0)
            {
                haulDestinations.Remove(destination);
            }
        }
    }
}