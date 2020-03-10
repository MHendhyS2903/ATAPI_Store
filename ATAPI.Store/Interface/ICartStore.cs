using ATAPI.Core.Model;
using ATAPI.Store.Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATAPI.Store.Interface
{
    public interface ICartStore
    {
        Task<ResponseModel<IEnumerable<StoreCartModel>>> GetByToken(string usertoken);

        ResponseModel<StoreCartModel> GetByTokenTest(string usertoken);

        Task<ResponseModel<int>> Add(string usertoken, ObjectId iditem, StoreCartModel entity);


        ResponseModel<int> AddTest(string usertoken, ObjectId iditem, StoreCartModel entity);

        Task<ResponseModel<StoreCartModel>> Update(StoreCartModel entity, string usertoken, string iditem);


        ResponseModel<StoreCartModel> UpdateTest(StoreCartModel entity, string usertoken, string iditem);


        Task<ResponseModel<StoreCartModel>> Delete(ObjectId id, string usertoken);

        ResponseModel<StoreCartModel> DeleteTest(ObjectId id, string usertoken);

    }
}
