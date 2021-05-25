using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace FlickableStorage
{
    [HarmonyPatch(typeof(Building_Storage), "GetGizmos")]
    internal class Building_Storage_GetGizmos
    {
        private static void Postfix(ref IEnumerable<Gizmo> __result, Building_Storage __instance)
        {
            var gizmos = __result.ToList();
            gizmos.Add(FlickableStorage.GetStorageCommandAction(__instance));
            __result = gizmos;
        }
    }
}