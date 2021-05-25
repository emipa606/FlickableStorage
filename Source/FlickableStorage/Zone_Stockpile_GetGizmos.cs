using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace FlickableStorage
{
    [HarmonyPatch(typeof(Zone_Stockpile), "GetGizmos")]
    internal class Zone_Stockpile_GetGizmos
    {
        private static void Postfix(ref IEnumerable<Gizmo> __result, Zone_Stockpile __instance)
        {
            var gizmos = __result.ToList();
            gizmos.Add(FlickableStorage.GetStockpileCommandAction(__instance));
            __result = gizmos;
        }
    }
}