using HarmonyLib;
using RimWorld;
using Verse;

namespace FlickableStorage
{
    [HarmonyPatch(typeof(ForbidUtility), "IsForbidden", typeof(Thing), typeof(Faction))]
    internal class ForbidUtility_IsForbidden_Thing
    {
        private static void Postfix(ref Thing t, ref bool __result)
        {
            if (__result)
            {
                return;
            }

            __result = IsItemLocked(t);
        }

        private static bool IsItemLocked(Thing item)
        {
            if (item == null || !item.Spawned)
            {
                return false;
            }

            if (!(item is ThingWithComps thingWithComps))
            {
                return false;
            }

            var comp = thingWithComps.GetComp<CompForbiddable>();
            return comp != null && IsPositionLocked(item.Position, item.Map);
        }


        private static bool IsPositionLocked(IntVec3 position, Map map)
        {
            var parent = position.GetSlotGroup(map)?.parent;
            var storageTracker = parent?.Map.GetStorageTracker();
            if (storageTracker == null)
            {
                return false;
            }

            var destination = (IHaulDestination) parent;
            if (!storageTracker.Has(destination) ||
                storageTracker[destination] == 0 ||
                storageTracker[destination] == 3)
            {
                return false;
            }

            return true;
        }
    }
}