using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATAPI.Store.Model
{
    public class StoreShipmentModel
    {
        public ObjectId courier_id { get; set; }
        public ObjectId store_id { get; set; }
        public double weight { get; set; }
        public double price { get; set; }
        public double distance { get; set; }
        public string sender_name { get; set; }
        public string sender_address { get; set; }
        public string sender_cellular { get; set; }
        public double sender_latitude { get; set; }
        public double sender_longitude { get; set; }
        public string sender_city { get; set; }
        public ObjectId sender_city_id { get; set; }
        public string receiver_name { get; set; }
        public string receiver_address { get; set; }
        public string receiver_cellular { get; set; }
        public double receiver_latitude { get; set; }
        public double receiver_longitude { get; set; }
        public string receiver_city { get; set; }
        public ObjectId receiver_city_id { get; set; }
    }
}
