using System.Collections.Generic;
using System.Linq;
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
            set => haulDestinations[destination] = value;
        }

        public bool Has(IHaulDestination zone)
        {
            return haulDestinations.ContainsKey(zone);
        }

        public override void ExposeData()
        {
            base.ExposeData();
            if (Scribe.mode == LoadSaveMode.Saving)
            {
                Cull();
            }

            Scribe_Collections.Look(ref haulDestinations, "StockpileStatuses", LookMode.Reference, LookMode.Value,
                ref tmpHaulDestinationsKeys, ref tmpHaulDestinationValues);
        }

        private void Cull()
        {
            foreach (var destination in haulDestinations.Keys.ToList())
            {
                if (destination == null)
                {
                    continue;
                }

                var inPlace = map.haulDestinationManager.AllHaulDestinations
                    .FirstOrDefault(d => d.Position == destination.Position);
                if (inPlace != destination || haulDestinations[destination] == 0)
                {
                    haulDestinations.Remove(destination);
                }
            }
        }
    }
}