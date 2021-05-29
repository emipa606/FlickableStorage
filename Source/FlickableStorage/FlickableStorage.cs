using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace FlickableStorage
{
    [StaticConstructorOnStartup]
    public static class FlickableStorage
    {
        internal static readonly Texture2D FlickOnGizmo = ContentFinder<Texture2D>.Get("UI/FlickOnGizmo");
        internal static readonly Texture2D FlickOffGizmo = ContentFinder<Texture2D>.Get("UI/FlickOffGizmo");
        internal static readonly Texture2D FlickInGizmo = ContentFinder<Texture2D>.Get("UI/FlickInGizmo");
        internal static readonly Texture2D FlickOutGizmo = ContentFinder<Texture2D>.Get("UI/FlickOutGizmo");

        internal static readonly List<Type> targets;

        private static readonly Dictionary<Map, StorageTracker> storageTrackerCache =
            new Dictionary<Map, StorageTracker>();

        static FlickableStorage()
        {
            targets = GenTypes.AllTypes.Where(IsHaulDestinationImplementationWithGizmos).ToList();
            Log.Message($"[FlickableStorage] Found storage-classes: {string.Join(", ", targets)}");
            var harmony = new Harmony("Mlie.FlickableStorage");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        private static bool IsHaulDestinationImplementationWithGizmos(Type t)
        {
            return !t.IsAbstract
                   && t.GetInterfaces().Contains(typeof(IHaulDestination))
                   && AccessTools.Method(t, "GetGizmos") != null;
        }

        internal static StorageTracker GetStorageTracker(this Map map)
        {
            if (map == null)
            {
                return null;
            }

            StorageTracker value;

            if (!storageTrackerCache.ContainsKey(map))
            {
                value = storageTrackerCache[map] = map.GetComponent<StorageTracker>();
            }
            else
            {
                value = storageTrackerCache[map];
            }

            return value;
        }
    }
}