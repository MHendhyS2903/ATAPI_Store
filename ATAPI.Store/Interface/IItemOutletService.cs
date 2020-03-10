using ATAPI.Core.Model;
using ATAPI.Store.Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATAPI.Store.Interface
{
    public interface IItemOutletService
    {
        Task<ResponseModel<int>> Add(string token, StoreOutletModel entity);
        Task<ResponseModel<long>> Delete(string token, string id);
        Task<ResponseModel<StoreOutletModel>> GetByID(string token, ObjectId id);
        Task<ResponseModel<IEnumerable<StoreOutletModel>>> GetByParamns(string auth, string display, string filterparams);
        Task<ResponseModel<IEnumerable<StoreOutletModel>>> GetByToken(string token);
        //Task<ResponseModel<IEnumerable<RestaurantOutletInfoModel>>> GetNearBy(string latitude, string longitude, string token);
        Task<ResponseModel<long>> Update(string token, StoreOutletModel entity);  
    }
}
