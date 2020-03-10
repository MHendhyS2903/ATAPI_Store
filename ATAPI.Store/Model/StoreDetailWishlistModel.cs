using System;
using System.Collections.Generic;
using System.Text;

namespace ATAPI.Store.Model
{
    public class StoreDetailWishlistModel : StoreWishlistModel
    {
        public StoreItemModel [] details { get; set; }
    }
}
