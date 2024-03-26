using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace FlickableStorage;

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
        targets = targets.Where(type => type.ToString() != "PresetFilteredZones.Zone_PresetStockpile").ToList();
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

        if (!storageTrackerCache.TryGetValue(map, out var storageTracker))
        {
            value = storageTrackerCache[map] = map.GetComponent<StorageTracker>();
        }
        else
        {
            value = storageTracker;
        }

        return value;
    }
}