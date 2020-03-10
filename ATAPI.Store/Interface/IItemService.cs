using ATAPI.Core.Model;
using ATAPI.Store.Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATAPI.Store.Interface
{
    public interface IItemService
    {
        Task<ResponseModel<IEnumerable<StoreItemModel>>> GetByParamns(string auth, string display, string filterparams);
        ResponseModel<IEnumerable<StoreItemModel>> GetByParamnsTest(string auth, string display, string filterparams);



        Task<ResponseModel<StoreItemModel>> GetByID(ObjectId id);
        ResponseModel<StoreItemModel> GetByIDTest(ObjectId id);



        Task<ResponseModel<IEnumerable<StoreItemModel>>> GetByStore(ObjectId storeid);
        ResponseModel<IEnumerable<StoreItemModel>> GetByStoreTest(ObjectId storeid);



        Task<ResponseModel<IEnumerable<StoreItemModel>>> GetByCategory(ObjectId categoryid);
        ResponseModel<IEnumerable<StoreItemModel>> GetByCategoryTest(ObjectId categoryid);


        Task<ResponseModel<int>> Add(string token, StoreItemModel entity);
        ResponseModel<int> AddTest(string token, StoreItemModel entity);


        Task<ResponseModel<long>> Update(string token, StoreItemModel entity);
        ResponseModel<long> UpdateTest(string token, StoreItemModel entity);



        Task<ResponseModel<StoreItemModel>> Delete(string token, ObjectId id);
        ResponseModel<StoreItemModel> DeleteTest(string token, ObjectId id);
















    }
}
