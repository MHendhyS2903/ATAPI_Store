using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATAPI.Store.Model
{
    public class StoreItemCategoryModel
    {
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? updated_at { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? created_at { get; set; }

        public ObjectId id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }
}
