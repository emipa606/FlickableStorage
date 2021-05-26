using System.Reflection;
using HarmonyLib;
using Multiplayer.API;
using RimWorld;
using UnityEngine;
using Verse;

namespace FlickableStorage
{
    [StaticConstructorOnStartup]
    public class FlickableStorage
    {
        private static readonly Texture2D FlickOnGizmo = ContentFinder<Texture2D>.Get("UI/FlickOnGizmo");
        private static readonly Texture2D FlickOffGizmo = ContentFinder<Texture2D>.Get("UI/FlickOffGizmo");
        private static readonly Texture2D FlickInGizmo = ContentFinder<Texture2D>.Get("UI/FlickInGizmo");
        private static readonly Texture2D FlickOutGizmo = ContentFinder<Texture2D>.Get("UI/FlickOutGizmo");

        static FlickableStorage()
        {
            var harmony = new Harmony("Mlie.FlickableStorage");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            if (MP.enabled)
            {
                MP.RegisterAll();
            }
        }

        public static Command_Action GetStorageCommandAction(Building_Storage storage)
        {
            var storageTracker = storage.Map.GetComponent<StorageTracker>();
            if (storageTracker == null)
            {
                Log.ErrorOnce("[FlickableStorage]: Could not find StorageTracker for map, this should not happen.",
                    storage.GetHashCode());
                return null;
            }

            if (!storageTracker.Has(storage))
            {
                storageTracker[storage] = 0;
            }

            switch (storageTracker[storage])
            {
                case 1:
                    return new Command_Action
                    {
                        icon = FlickOffGizmo,
                        defaultLabel = "FlickableStorage.Label.Off".Translate(),
                        defaultDesc = "FlickableStorage.Description.Off".Translate(),
                        action = delegate { storageTracker[storage] = 2; }
                    };
                case 2:
                    return new Command_Action
                    {
                        icon = FlickInGizmo,
                        defaultLabel = "FlickableStorage.Label.In".Translate(),
                        defaultDesc = "FlickableStorage.Description.In".Translate(),
                        action = delegate { storageTracker[storage] = 3; }
                    };
                case 3:
                    return new Command_Action
                    {
                        icon = FlickOutGizmo,
                        defaultLabel = "FlickableStorage.Label.Out".Translate(),
                        defaultDesc = "FlickableStorage.Description.Out".Translate(),
                        action = delegate { storageTracker[storage] = 0; }
                    };
                default:
                    return new Command_Action
                    {
                        icon = FlickOnGizmo,
                        defaultLabel = "FlickableStorage.Label.On".Translate(),
                        defaultDesc = "FlickableStorage.Description.On".Translate(),
                        action = delegate { storageTracker[storage] = 1; }
                    };
            }
        }

        public static Command_Action GetStockpileCommandAction(Zone_Stockpile stockpile)
        {
            var storageTracker = stockpile.Map.GetComponent<StorageTracker>();
            if (storageTracker == null)
            {
                Log.ErrorOnce("[FlickableStorage]: Could not find StorageTracker for map, this should not happen.",
                    stockpile.GetHashCode());
                return null;
            }

            if (!storageTracker.Has(stockpile))
            {
                storageTracker[stockpile] = 0;
            }

            switch (storageTracker[stockpile])
            {
                case 1:
                    return new Command_Action
                    {
                        icon = FlickOffGizmo,
                        defaultLabel = "FlickableStorage.Label.Off".Translate(),
                        defaultDesc = "FlickableStorage.Description.Off".Translate(),
                        action = delegate { storageTracker[stockpile] = 2; }
                    };
                case 2:
                    return new Command_Action
                    {
                        icon = FlickInGizmo,
                        defaultLabel = "FlickableStorage.Label.In".Translate(),
                        defaultDesc = "FlickableStorage.Description.In".Translate(),
                        action = delegate { storageTracker[stockpile] = 3; }
                    };
                case 3:
                    return new Command_Action
                    {
                        icon = FlickOutGizmo,
                        defaultLabel = "FlickableStorage.Label.Out".Translate(),
                        defaultDesc = "FlickableStorage.Description.Out".Translate(),
                        action = delegate { storageTracker[stockpile] = 0; }
                    };
                default:
                    return new Command_Action
                    {
                        icon = FlickOnGizmo,
                        defaultLabel = "FlickableStorage.Label.On".Translate(),
                        defaultDesc = "FlickableStorage.Description.On".Translate(),
                        action = delegate { storageTracker[stockpile] = 1; }
                    };
            }
        }

        public static bool IsItemLocked(Thing item)
        {
            if (item == null || !item.Spawned)
            {
                return false;
            }

            return IsPositionLocked(item.Position, item.Map);
        }


        private static bool IsPositionLocked(IntVec3 position, Map map)
        {
            var parent = position.GetSlotGroup(map)?.parent;
            var storageTracker = parent?.Map.GetComponent<StorageTracker>();
            if (storageTracker == null)
            {
                return false;
            }

            if (parent is Zone_Stockpile parentStockpile)
            {
                if (!storageTracker.Has(parentStockpile) ||
                    storageTracker[parentStockpile] == 0 ||
                    storageTracker[parentStockpile] == 3)
                {
                    return false;
                }

                return true;
            }

            if (parent is Building_Storage parentStorage)
            {
                if (!storageTracker.Has(parentStorage) ||
                    storageTracker[parentStorage] == 0 ||
                    storageTracker[parentStorage] == 3)
                {
                    return false;
                }

                return true;
            }

            return false;
        }
    }
}