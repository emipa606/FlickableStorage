﻿using HarmonyLib;
using RimWorld;
using Verse;

namespace FlickableStorage
{
    [HarmonyPatch(typeof(Building_Storage), "Accepts", typeof(Thing))]
    internal class Building_Storage_Accepts
    {
        private static bool Prefix(Building_Storage __instance, bool __result)
        {
            var storageTracker = __instance.Map.GetComponent<StorageTracker>();
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