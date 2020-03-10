using ATAPI.Core.Model;
using ATAPI.Store.Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATAPI.Store.Interface
{
    public interface IWishlistStore
    {

        Task<ResponseModel<IEnumerable<StoreWishlistModel>>> GetByToken(string usertoken);
        ResponseModel<StoreWishlistModel> GetByTokenTest(string usertoken);


        Task<ResponseModel<IEnumerable<StoreWishlistModel>>> GetByParamns(string auth, string display, string filterparams);
        ResponseModel<IEnumerable<StoreWishlistModel>> GetByParamnsTest(string auth, string display, string filterparams);

        Task<ResponseModel<int>> Add(string usertoken, ObjectId iditem, StoreWishlistModel entity);
        ResponseModel<int> AddTest(string usertoken, ObjectId iditem, StoreWishlistModel entity);


        Task<ResponseModel<StoreWishlistModel>> Delete(ObjectId id, string usertoken);
        ResponseModel<StoreWishlistModel> DeleteTest(ObjectId id, string usertoken);



    }
}
