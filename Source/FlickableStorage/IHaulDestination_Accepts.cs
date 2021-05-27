using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace FlickableStorage
{
    [HarmonyPatch]
    internal class IHaulDestination_Accepts
    {
        private static IEnumerable<MethodBase> TargetMethods()
        {
            foreach(var type in FlickableStorage.targets) {
                yield return AccessTools.Method(type, nameof(IHaulDestination.Accepts));
            }
        }

        private static bool Prefix(IHaulDestination __instance, bool __result)
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
}