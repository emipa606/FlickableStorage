using HarmonyLib;
using RimWorld;

namespace FlickableStorage;

[HarmonyPatch(typeof(StoreUtility), "TryFindBestBetterStoreCellForWorker")]
internal class StoreUtility_TryFindBestBetterStoreCellForWorker
{
    private static bool Prefix(ISlotGroup slotGroup)
    {
        if (slotGroup.Settings.owner is not IHaulDestination destination)
        {
            return true;
        }

        var storageTracker = destination.Map.GetStorageTracker();
        if (storageTracker == null)
        {
            return true;
        }

        return !storageTracker.Has(destination) ||
               storageTracker[destination] == 0 || storageTracker[destination] == 2;
    }
}