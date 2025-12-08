using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace FlickableStorage;

[HarmonyPatch]
internal class IHaulDestination_HaulDestinationEnabled
{
    public static IEnumerable<MethodBase> TargetMethods()
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

    public static bool Prefix(IHaulDestination __instance, ref bool __result)
    {
        var storageTracker = GlobalStorageTracker.Instance;
        if (storageTracker == null)
        {
            return true;
        }

        if (!storageTracker.Has(__instance, out var status) || status == 0 || status == 2)
        {
            return true;
        }

        __result = false;
        return false;
    }
}