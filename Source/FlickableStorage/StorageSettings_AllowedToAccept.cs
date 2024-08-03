using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace FlickableStorage;

[HarmonyPatch]
internal class StorageSettings_AllowedToAccept
{
    private static IEnumerable<MethodBase> TargetMethods()
    {
        yield return AccessTools.Method(typeof(StorageSettings), nameof(StorageSettings.AllowedToAccept),
            [typeof(Thing)]);
        yield return AccessTools.Method(typeof(StorageSettings), nameof(StorageSettings.AllowedToAccept),
            [typeof(ThingDef)]);
    }

    private static bool Prefix(StorageSettings __instance, ref bool __result)
    {
        if (__instance.owner is not IHaulDestination destination)
        {
            return true;
        }

        var storageTracker = destination.Map.GetStorageTracker();
        if (storageTracker == null)
        {
            return true;
        }

        if (!storageTracker.Has(destination) ||
            storageTracker[destination] == 0 || storageTracker[destination] == 2)
        {
            return true;
        }

        __result = false;
        return false;
    }
}