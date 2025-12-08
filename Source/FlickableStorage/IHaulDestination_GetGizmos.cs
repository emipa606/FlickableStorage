using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace FlickableStorage;

[HarmonyPatch]
internal class IHaulDestination_GetGizmos
{
    public static IEnumerable<MethodBase> TargetMethods()
    {
        foreach (var type in FlickableStorage.Targets)
        {
            yield return AccessTools.Method(type, "GetGizmos");
        }
    }

    public static IEnumerable<Gizmo> Postfix(IEnumerable<Gizmo> __result, object __instance)
    {
        foreach (var gizmo in __result)
        {
            yield return gizmo;
        }

        if (__instance is IHaulDestination destination)
        {
            yield return getCommandAction(destination);
        }
    }

    private static Command_Action getCommandAction(IHaulDestination destination)
    {
        var storageTracker = GlobalStorageTracker.Instance;

        if (storageTracker == null)
        {
            if (destination != null)
            {
                Log.ErrorOnce("[FlickableStorage]: Could not find StorageTracker for map, this should not happen.",
                    destination.GetHashCode());
            }

            return null;
        }

        var current = storageTracker.Has(destination, out var status) ? status : 0;

        switch (current)
        {
            case 1:
                return new Command_Action
                {
                    icon = FlickableStorage.FlickOffGizmo,
                    defaultLabel = "FlickableStorage.Label.Off".Translate(),
                    defaultDesc = "FlickableStorage.Description.Off".Translate(),
                    action = delegate { storageTracker.UpdateDestinations(destination, 2); }
                };
            case 2:
                return new Command_Action
                {
                    icon = FlickableStorage.FlickInGizmo,
                    defaultLabel = "FlickableStorage.Label.In".Translate(),
                    defaultDesc = "FlickableStorage.Description.In".Translate(),
                    action = delegate { storageTracker.UpdateDestinations(destination, 3); }
                };
            case 3:
                return new Command_Action
                {
                    icon = FlickableStorage.FlickOutGizmo,
                    defaultLabel = "FlickableStorage.Label.Out".Translate(),
                    defaultDesc = "FlickableStorage.Description.Out".Translate(),
                    action = delegate { storageTracker.UpdateDestinations(destination, 0); }
                };
            default:
                return new Command_Action
                {
                    icon = FlickableStorage.FlickOnGizmo,
                    defaultLabel = "FlickableStorage.Label.On".Translate(),
                    defaultDesc = "FlickableStorage.Description.On".Translate(),
                    action = delegate { storageTracker.UpdateDestinations(destination, 1); }
                };
        }
    }
}