using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ATAPI.Store.Model
{
    [BsonIgnoreExtraElements]
    public class StoreWishlistModel : StoreItemModel
    {
        public ObjectId iditem { get; set; }
        public string usertoken { get; set; }
    }
}
