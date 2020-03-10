using System;
using System.Collections.Generic;
using System.Text;

namespace ATAPI.Store.Model
{
    public class StoreInfoModel : StoreModel 
    {
        public double balance { get; set; }
        public List<StoreOutletInfoModel> outlets { get; set; }
    }
}
   