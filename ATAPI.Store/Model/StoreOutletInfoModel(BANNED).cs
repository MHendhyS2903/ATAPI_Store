using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATAPI.Store.Model
{
    public class StoreOutletInfoModel : IStoreOutlet
    {
        public ObjectId id { get; set; }
        public ObjectId idstore { get; set; }
        public string outletname { get; set; }
        public int outletcity { get; set; }
        public string outletaddress { get; set; }
        public string outletbuilding { get; set; }
        public string outletcategory { get; set; }
        public string outletcellular { get; set; }
        public string outletemail { get; set; }
        public string photo { get; set; }
        public string icon { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public double distance { get; set; }
        public List<StoreItemModel> items { get; set; }
    }
}
