using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATAPI.Store.Interface;
using ATAPI.Store.Model;
using Microsoft.AspNetCore.Mvc;
using ATAPI.Model;
using Newtonsoft.Json;
using MongoDB.Bson;

namespace ATAPI.Store.Run.Controllers
{
    public class OrderController : Controller
    {
        IOrderStore _storeBL;
        JsonSerializerSettings _jsonSettings;

        public OrderController(IServiceProvider serviceProvider)
        {
            _jsonSettings = new JsonSerializerSettings();
            _jsonSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";

            _storeBL = (IOrderStore)serviceProvider.GetService(typeof(IOrderStore));
        }


        public async Task<ContentResult> GetByID(string id)
        {
            var result = await _storeBL.GetByID(new ObjectId(id));
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }

        public async Task<ContentResult> GetByCurrentTransaction(string iduser)
        {
            var result = await _storeBL.GetByCurrentTransaction(iduser);
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }

        public async Task<ContentResult> GetByActiveTransaction(string id)
        {
            var result = await _storeBL.GetByActiveTransaction(id);
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }

        public async Task<ContentResult> GetByUnpaidTransaction(string iduser)
        {
            var result = await _storeBL.GetByUnpaidTransaction(iduser);
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }

        public async Task<ContentResult> GetByFinishedTransaction(string id)
        {
            var result = await _storeBL.GetByFinishedTransaction(id);
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }

        public async Task<ContentResult> GetByOrderId(string id)
        {
            var result = await _storeBL.GetByOrderId(id);
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }

        public async Task<ContentResult> Add(StoreOrderModel entity)
        {
            var result = await _storeBL.Add(entity);
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }

        public async Task<ContentResult> Update(StoreOrderModel entity, string orderid, string token)
        {
            var result = await _storeBL.Update(entity, orderid, token);
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }







    }
}