using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATAPI.Store.Model
{
    public class StoreCourierModel
    {
        public ObjectId id { get; set; }
        public string courier { get; set; }
        public string company_name { get; set; }
        public Nullable<DateTime> created_at { get; set; }
        public Nullable<DateTime> updated_at { get; set; }
    }
}
