using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace FlickableStorage
{
    [HarmonyPatch]
    internal class IHaulDestination_GetGizmos
    {
        private static IEnumerable<MethodBase> TargetMethods()
        {
            foreach (var type in FlickableStorage.targets)
            {
                yield return AccessTools.Method(type, "GetGizmos");
            }
        }

        private static void Postfix(ref IEnumerable<Gizmo> __result, IHaulDestination __instance)
        {
            var gizmos = __result.ToList();
            gizmos.Add(GetCommandAction(__instance));
            __result = gizmos;
        }

        private static Command_Action GetCommandAction(IHaulDestination destination)
        {
            var storageTracker = destination.Map.GetStorageTracker();

            if (storageTracker == null)
            {
                Log.ErrorOnce("[FlickableStorage]: Could not find StorageTracker for map, this should not happen.",
                    destination.GetHashCode());
                return null;
            }

            int current;
            if (storageTracker.Has(destination))
            {
                current = storageTracker[destination];
            }
            else
            {
                current = 0;
            }

            switch (current)
            {
                case 1:
                    return new Command_Action
                    {
                        icon = FlickableStorage.FlickOffGizmo,
                        defaultLabel = "FlickableStorage.Label.Off".Translate(),
                        defaultDesc = "FlickableStorage.Description.Off".Translate(),
                        action = delegate { storageTracker[destination] = 2; }
                    };
                case 2:
                    return new Command_Action
                    {
                        icon = FlickableStorage.FlickInGizmo,
                        defaultLabel = "FlickableStorage.Label.In".Translate(),
                        defaultDesc = "FlickableStorage.Description.In".Translate(),
                        action = delegate { storageTracker[destination] = 3; }
                    };
                case 3:
                    return new Command_Action
                    {
                        icon = FlickableStorage.FlickOutGizmo,
                        defaultLabel = "FlickableStorage.Label.Out".Translate(),
                        defaultDesc = "FlickableStorage.Description.Out".Translate(),
                        action = delegate { storageTracker[destination] = 0; }
                    };
                default:
                    return new Command_Action
                    {
                        icon = FlickableStorage.FlickOnGizmo,
                        defaultLabel = "FlickableStorage.Label.On".Translate(),
                        defaultDesc = "FlickableStorage.Description.On".Translate(),
                        action = delegate { storageTracker[destination] = 1; }
                    };
            }
        }
    }
}