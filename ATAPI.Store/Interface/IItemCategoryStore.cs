using ATAPI.Core.Model;
using ATAPI.Store.Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATAPI.Store.Interface
{
    public interface IItemCategoryStore
    {
        Task<ResponseModel<IEnumerable<StoreItemCategoryModel>>> GetByParamns(string auth, string display, string filterparams);
        ResponseModel<IEnumerable<StoreItemCategoryModel>> GetByParamnsTest(string auth, string display, string filterparams);



        Task<ResponseModel<StoreItemCategoryModel>> GetByID(ObjectId id);
        ResponseModel<StoreItemCategoryModel> GetByIDTest(ObjectId id);


        Task<ResponseModel<StoreItemCategoryModel>> Add(string token, StoreItemCategoryModel entity);
        ResponseModel<StoreItemCategoryModel> AddTest(string token, StoreItemCategoryModel entity);


        Task<ResponseModel<long>> Update(string token, StoreItemCategoryModel entity);
        ResponseModel<long> UpdateTest(string token, StoreItemCategoryModel entity);



        Task<ResponseModel<StoreItemCategoryModel>> Delete(string token, ObjectId id);
        ResponseModel<StoreItemCategoryModel> DeleteTest(string token, ObjectId id);







    }
}
