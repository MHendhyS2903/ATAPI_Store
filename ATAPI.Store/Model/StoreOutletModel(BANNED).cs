using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATAPI.Store.Model
{
    public class StoreOutletModel : IStoreOutlet
    {
        public string updatedby { get; set; }
        public string createdby { get; set; }
        public double lng { get; set; }
        public double lat { get; set; }
        public string status { get; set; }
        public string icon { get; set; }
        public string photo { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? created_at { get; set; }
        public string outletemail { get; set; }
        public string outletcategory { get; set; }
        public string outletbuilding { get; set; }
        public string outletaddress { get; set; }
        public int outletcity { get; set; }
        public string outletname { get; set; }
        public ObjectId idstore { get; set; }
        public ObjectId id { get; set; }
        public string outletcellular { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? updated_at { get; set; }
    }
}
