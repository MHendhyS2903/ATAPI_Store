using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATAPI.Store.Model
{
    public class StoreItemModel
    {
        public ObjectId id { get; set; }
        public ObjectId idstore { get; set; }
        public ObjectId idcategory { get; set; }
        public string title { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public string photo { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? created_at { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? updated_at { get; set; }
    }
}
