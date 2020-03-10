using ATAPI.Core.Model;
using ATAPI.Store.Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATAPI.Store.Interface
{
    public interface IAccountStore
    {

        Task<ResponseModel<long>> IsExists(StoreAccountModel entity);
        ResponseModel<long> IsExistsTest(StoreAccountModel entity);


        Task<ResponseModel<IEnumerable<StoreAccountModel>>> GetByParams(string auth, string display, string filterparams);
        ResponseModel<IEnumerable<StoreAccountModel>> GetByParamsTest(string auth, string display, string filterparams);


        Task<ResponseModel<StoreAccountModel>> GetByToken(string token, ObjectId id);
        ResponseModel<StoreAccountModel> GetByTokenTest(string token, ObjectId id);


        Task<ResponseModel<int>> Registration(StoreAccountModel entity);
        ResponseModel<int> RegistrationTest(StoreAccountModel entity);


        Task<ResponseModel<StoreAccountModel>> Update(StoreAccountModel entity, string token);
        ResponseModel<StoreAccountModel> UpdateTest(StoreAccountModel entity, string token);


        Task<ResponseModel<StoreAccountModel>> Login(string cellular, string password);
        ResponseModel<StoreAccountModel> LoginTest(string cellular, string password);





    }
}
