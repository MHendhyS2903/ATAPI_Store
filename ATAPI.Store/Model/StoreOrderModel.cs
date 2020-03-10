using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATAPI.Store.Model
{
    public class StoreOrderModel
    {
        public string reason { get; set; }
        public string status { get; set; }
        public double price { get; set; }


        public string destination { get; set; }
        public double distance { get; set; }


        public double lngfrom { get; set; }
        public double latfrom { get; set; }
        public double lngto { get; set; }
        public double latto { get; set; }


        public string orderid { get; set; }

        public ObjectId id { get; set; }

        public List<StoreCartModel> cart{ get; set; }
        public List<StoreShipmentModel> shipment { get; set; }


        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? created_at { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? updated_at { get; set; }

        public StoreOrderModel() {
            cart = new List<StoreCartModel>();
            shipment = new List<StoreShipmentModel>();
        }
    }
}
