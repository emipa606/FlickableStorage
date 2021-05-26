using System.Collections.Generic;
using RimWorld;
using Verse;

namespace FlickableStorage
{
    public class StorageTracker : MapComponent
    {
        private List<Zone_Stockpile> stockpileKeys;
        private Dictionary<Zone_Stockpile, int> StockpileStatuses = new Dictionary<Zone_Stockpile, int>();
        private List<int> stockpileValues;
        private List<Building_Storage> storageKeys;
        private Dictionary<Building_Storage, int> StorageStatuses = new Dictionary<Building_Storage, int>();
        private List<int> storageValues;

        public StorageTracker(Map map) : base(map)
        {
        }

        public int this[Zone_Stockpile zone]
        {
            get {
                return StockpileStatuses[zone];
            }
            set {
                StockpileStatuses[zone] = value;
            }
        }

        public int this[Building_Storage building]
        {
            get {
                return StorageStatuses[building];
            }
            set {
                StorageStatuses[building] = value;
            }
        }

        public bool Has(Zone_Stockpile zone)
        {
            return StockpileStatuses.ContainsKey(zone);
        }

        public bool Has(Building_Storage building)
        {
            return StorageStatuses.ContainsKey(building);
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref StorageStatuses, "StorageStatuses", LookMode.Reference, LookMode.Value,
                ref storageKeys, ref storageValues);
            Scribe_Collections.Look(ref StockpileStatuses, "StockpileStatuses", LookMode.Reference, LookMode.Value,
                ref stockpileKeys, ref stockpileValues);
        }
    }
}