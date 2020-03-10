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
    public class ItemController : Controller
    {
        IItemService _storeBL;
        JsonSerializerSettings _jsonSettings;

        public ItemController(IServiceProvider serviceProvider)
        {
            _jsonSettings = new JsonSerializerSettings();
            _jsonSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";

            _storeBL = (IItemService)serviceProvider.GetService(typeof(IItemService));
        }

        public async Task<ContentResult> GetByParamns(string auth, string display, string filterparams)
        {
            var result = await _storeBL.GetByParamns(auth, display, filterparams);
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }

        public async Task<ContentResult> GetByID(string id)
        {
            var result = await _storeBL.GetByID(new ObjectId(id));
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }

        public async Task<ContentResult> GetByStore(string storeid)
        {
            var result = await _storeBL.GetByStore(new ObjectId(storeid));
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }

        public async Task<ContentResult> GetByCategory(string categoryid)
        {
            var result = await _storeBL.GetByCategory(new ObjectId(categoryid));
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }

        public async Task<ContentResult> Add(string token, StoreItemModel entity)
        {
            var result = await _storeBL.Add(token, entity);
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }

        public async Task<ContentResult> Update(string token, StoreItemModel entity)
        {
            var result = await _storeBL.Update(token, entity);
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }

        public async Task<ContentResult> Delete(string token, string id)
        {
            var result = await _storeBL.Delete(token, new ObjectId(id));
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }
    }
}