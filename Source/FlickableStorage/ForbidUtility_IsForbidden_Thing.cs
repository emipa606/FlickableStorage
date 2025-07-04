using HarmonyLib;
using RimWorld;
using Verse;

namespace FlickableStorage;

[HarmonyPatch(typeof(ForbidUtility), nameof(ForbidUtility.IsForbidden), typeof(Thing), typeof(Faction))]
internal class ForbidUtility_IsForbidden_Thing
{
    private static void Postfix(ref Thing t, ref bool __result)
    {
        if (__result)
        {
            return;
        }

        __result = isItemLocked(t);
    }

    private static bool isItemLocked(Thing item)
    {
        if (item is not { Spawned: true })
        {
            return false;
        }

        switch (item)
        {
            case Blueprint:
            case Frame:
                break;
            case ThingWithComps thingWithComps:
            {
                var comp = thingWithComps.GetComp<CompForbiddable>();
                return comp != null && isPositionLocked(item.Position, item.Map, item);
            }
        }

        return false;
    }


    private static bool isPositionLocked(IntVec3 position, Map map, Thing thing)
    {
        var parent = position.GetSlotGroup(map)?.parent;
        var storageTracker = parent?.Map.GetStorageTracker();
        if (storageTracker == null)
        {
            return false;
        }

        var destination = (IHaulDestination)parent;
        if (!storageTracker.Has(destination) ||
            storageTracker[destination] == 0 ||
            storageTracker[destination] == 3)
        {
            return false;
        }

        return destination.GetStoreSettings().AllowedToAccept(thing);
    }
}