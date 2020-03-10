using ATAPI.Core.Model;
using ATAPI.Store.Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATAPI.Store.Interface
{
    public interface IOrderStore
    {
        Task<ResponseModel<StoreOrderModel>> GetByID(ObjectId id);
        ResponseModel<StoreOrderModel> GetByIDTest(ObjectId id);


        Task<ResponseModel<StoreOrderModel>> GetByCurrentTransaction(string orderid);
        ResponseModel<StoreOrderModel> GetByCurrentTransactionTest(string orderid);


        Task<ResponseModel<StoreOrderModel>> GetByActiveTransaction(string orderid);
        ResponseModel<StoreOrderModel> GetByActiveTransactionTest(string orderid);


        Task<ResponseModel<StoreOrderModel>> GetByUnpaidTransaction(string orderid);
        ResponseModel<StoreOrderModel> GetByUnpaidTransactionTest(string orderid);



        Task<ResponseModel<StoreOrderModel>> GetByFinishedTransaction(string orderid);
        ResponseModel<StoreOrderModel> GetByFinishedTransactionTest(string orderid);


        Task<ResponseModel<StoreOrderModel>> GetByOrderId(string orderid);
        ResponseModel<StoreOrderModel> GetByOrderIdTest(string orderid);


        Task<ResponseModel<StoreOrderModel>> Add(StoreOrderModel entity);
        ResponseModel<StoreOrderModel> AddTest(StoreOrderModel entity);



        Task<ResponseModel<StoreOrderModel>> Update(StoreOrderModel entity, string orderid, string token);
        ResponseModel<StoreOrderModel> UpdateTest(StoreOrderModel entity, string orderid, string token);









    }
}
