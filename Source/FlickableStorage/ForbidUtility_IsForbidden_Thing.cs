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

            __result = FlickableStorage.IsItemLocked(t);
        }
    }
}