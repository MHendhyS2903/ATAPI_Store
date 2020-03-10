using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATAPI.Store.Model
{
    public class StoreAppUsers
    {
        public ObjectId id { get; set; }
        public string idgoogle { get; set; }
        public string fullname { get; set; }
        public int domicilecity { get; set; }
        public string cellular { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string photo { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public string token { get; set; }
        public string fbtoken { get; set; }
        public string balance { get; set; }
        public string otpcode { get; set; }
        public string status { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? created_at { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? updated_at { get; set; }
        public string dokuid { get; set; }

        public string reference { get; set; }
    }
}
