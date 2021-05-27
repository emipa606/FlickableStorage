using System.Collections.Generic;
using Multiplayer.API;
using RimWorld;
using Verse;

namespace FlickableStorage
{
    public class StorageTracker : MapComponent
    {
        private Dictionary<IHaulDestination, int> haulDestinations = new Dictionary<IHaulDestination, int>();
        private List<IHaulDestination> tmpHaulDestinationsKeys;
        private List<int> tmpHaulDestinationValues;


        public StorageTracker(Map map) : base(map)
        {
        }

        public int this[IHaulDestination destination]
        {
            get => haulDestinations[destination];
            [SyncMethod] set => haulDestinations[destination] = value;
        }

        public bool Has(IHaulDestination zone)
        {
            return haulDestinations.ContainsKey(zone);
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref haulDestinations, "StockpileStatuses", LookMode.Reference, LookMode.Value, ref tmpHaulDestinationsKeys, ref tmpHaulDestinationValues);
        }
    }
}