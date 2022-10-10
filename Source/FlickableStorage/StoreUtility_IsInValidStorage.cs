using HarmonyLib;
using RimWorld;
using Verse;

namespace FlickableStorage;

[HarmonyPatch(typeof(StoreUtility), "IsInValidStorage", typeof(Thing))]
internal class StoreUtility_IsInValidStorage
{
    private static void Prefix(Thing t, out int __state)
    {
        __state = -1;
        var destination = StoreUtility.CurrentHaulDestinationOf(t);
        var storageTracker = destination?.Map.GetStorageTracker();
        if (storageTracker == null)
        {
            return;
        }

        if (!storageTracker.Has(destination) ||
            storageTracker[destination] == 0 || storageTracker[destination] == 2)
        {
            return;
        }

        __state = storageTracker[destination];
        storageTracker[destination] = 0;
    }

    private static void Postfix(Thing t, int __state)
    {
        if (__state == -1)
        {
            return;
        }

        var destination = StoreUtility.CurrentHaulDestinationOf(t);
        destination.Map.GetStorageTracker()[destination] = __state;
    }
}