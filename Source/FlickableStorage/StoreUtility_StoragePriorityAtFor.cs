using HarmonyLib;
using RimWorld;
using Verse;

namespace FlickableStorage
{
    [HarmonyPatch(typeof(StoreUtility), "StoragePriorityAtFor", typeof(IHaulDestination), typeof(Thing))]
    internal class StoreUtility_StoragePriorityAtFor
    {
        private static void Prefix(IHaulDestination at, Thing t, out int __state)
        {
            __state = -1;
            var destination = at;
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

        private static void Postfix(IHaulDestination at, Thing t, int __state)
        {
            if (__state == -1)
            {
                return;
            }

            var destination = at;
            destination.Map.GetStorageTracker()[destination] = __state;
        }
    }
}