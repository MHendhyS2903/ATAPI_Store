using ATAPI.Core.Model;
using ATAPI.Store.Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATAPI.Store.Interface
{
    public interface IRegistStore
    {

        Task<ResponseModel<long>> IsExists(StoreModel entity);
        ResponseModel<long> IsExistsTest(StoreModel entity);

        Task<ResponseModel<StoreModel>> GetByID(string token, ObjectId id);
        ResponseModel<StoreModel> GetByIDTest(string token, ObjectId id);


        Task<ResponseModel<IEnumerable<StoreModel>>> GetByParams(string auth, string display, string filterparams);
        ResponseModel<IEnumerable<StoreModel>> GetByParamsTest(string auth, string display, string filterparams);


        Task<ResponseModel<StoreModel>> GetByToken(string token, ObjectId id);
        ResponseModel<StoreModel> GetByTokenTest(string token, ObjectId id);


        Task<ResponseModel<int>> Registration(StoreModel entity);
        ResponseModel<int> RegistrationTest(StoreModel entity);


        Task<ResponseModel<StoreModel>> Update(StoreModel entity, string token);
        ResponseModel<StoreModel> UpdateTest(StoreModel entity, string token);

        Task<ResponseModel<StoreModel>> Delete(string token, ObjectId id);
        ResponseModel<StoreModel> DeleteTest(string token, ObjectId id);


        Task<ResponseModel<StoreModel>> Login(string cellular, string password);
        ResponseModel<StoreModel> LoginTest(string cellular, string password);





    }
}
