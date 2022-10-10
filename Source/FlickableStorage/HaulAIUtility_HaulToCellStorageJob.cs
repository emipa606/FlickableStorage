using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace FlickableStorage;

[HarmonyPatch(typeof(HaulAIUtility), "HaulToCellStorageJob")]
internal class HaulAIUtility_HaulToCellStorageJob
{
    private static bool Prefix(Thing t, IntVec3 storeCell, ref Job __result)
    {
        var storageTracker = t.Map.GetStorageTracker();
        if (storageTracker == null)
        {
            return true;
        }

        var destination = (IHaulDestination)storeCell.GetSlotGroup(t.Map)?.parent;

        if (!storageTracker.Has(destination) ||
            storageTracker[destination] == 0 || storageTracker[destination] == 2)
        {
            return true;
        }

        __result = null;
        return false;
    }
}