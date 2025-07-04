using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace FlickableStorage;

[HarmonyPatch]
internal class IHaulDestination_HaulDestinationEnabled
{
    private static IEnumerable<MethodBase> TargetMethods()
    {
        foreach (var type in FlickableStorage.Targets)
        {
            Log.Message($"[FlickableStorage]: Checking {type} for HaulDestinationEnabled property");
            var propertyGetter = AccessTools.PropertyGetter(type, "HaulDestinationEnabled");
            if (propertyGetter != null)
            {
                yield return propertyGetter;
                continue;
            }

            propertyGetter = AccessTools.PropertyGetter(type, "Rimworld.IHaulDestination.HaulDestinationEnabled");
            if (propertyGetter == null)
            {
                continue;
            }

            yield return propertyGetter;
        }
    }

    private static bool Prefix(IHaulDestination __instance, ref bool __result)
    {
        var storageTracker = __instance.Map.GetStorageTracker();
        if (storageTracker == null)
        {
            return true;
        }

        if (!storageTracker.Has(__instance) ||
            storageTracker[__instance] == 0 || storageTracker[__instance] == 2)
        {
            return true;
        }

        __result = false;
        return false;
    }
}