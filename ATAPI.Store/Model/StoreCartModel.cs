using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATAPI.Store.Model
{
    public class StoreCartModel : StoreItemModel
    {
        public string usertoken { get; set; }
        public ObjectId iditem { get; set; }

        public int qty { get; set; }

        public double subtotal { get; set; }
        public string notes { get; set; }

        public double GetTotal()
        {
            var subtotal = qty * price;
            return subtotal;
        }


    }
}
