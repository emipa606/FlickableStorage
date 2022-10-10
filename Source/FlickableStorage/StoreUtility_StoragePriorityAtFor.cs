using HarmonyLib;
using RimWorld;
using Verse;

namespace FlickableStorage;

[HarmonyPatch(typeof(StoreUtility), "StoragePriorityAtFor", typeof(IHaulDestination), typeof(Thing))]
internal class StoreUtility_StoragePriorityAtFor
{
    private static void Prefix(IHaulDestination at, out int __state)
    {
        __state = -1;
        var storageTracker = at?.Map.GetStorageTracker();
        if (storageTracker == null)
        {
            return;
        }

        if (!storageTracker.Has(at) ||
            storageTracker[at] == 0 || storageTracker[at] == 2)
        {
            return;
        }

        __state = storageTracker[at];
        storageTracker[at] = 0;
    }

    private static void Postfix(IHaulDestination at, int __state)
    {
        if (__state == -1)
        {
            return;
        }

        at.Map.GetStorageTracker()[at] = __state;
    }
}