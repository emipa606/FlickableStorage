using System.Linq;
using HarmonyLib;
using Multiplayer.API;
using RimWorld;
using Verse;

namespace FlickableStorage
{
    [StaticConstructorOnStartup]
    internal static class Multiplayer
    {
        static Multiplayer()
        {
            if (!MP.enabled)
            {
                return;
            }

            MP.RegisterSyncWorker<IHaulDestination>(IHaulDestinationWorker);
            MP.RegisterSyncMethod(AccessTools.Method(typeof(StorageTracker), "set_Item"));
        }

        private static void IHaulDestinationWorker(SyncWorker sync, ref IHaulDestination destination)
        {
            if (sync.isWriting)
            {
                sync.Write(destination.Map);
                sync.Write(destination.Position);
            }
            else
            {
                var map = sync.Read<Map>();
                var pos = sync.Read<IntVec3>();

                destination = map.haulDestinationManager.AllHaulDestinationsListForReading
                    .FirstOrDefault(d => d.Position == pos);
            }
        }
    }
}