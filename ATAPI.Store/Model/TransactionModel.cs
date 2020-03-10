using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATAPI.Store.Model
{
    class TransactionModel
    {
        public ObjectId id { get; set; }
        public string cellnumber { get; set; }
        public double credit { get; set; }
        public double balance { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string transactioncode { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? created_at { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? updated_at { get; set; }
        public string destination { get; set; }
    }
}
