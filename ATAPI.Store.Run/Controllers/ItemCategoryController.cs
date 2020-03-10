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
    public class ItemCategoryController : Controller
    {
        IItemCategoryStore _storeBL;
        JsonSerializerSettings _jsonSettings;

        public ItemCategoryController(IServiceProvider serviceProvider)
        {
            _jsonSettings = new JsonSerializerSettings();
            _jsonSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";

            _storeBL = (IItemCategoryStore)serviceProvider.GetService(typeof(IItemCategoryStore));
        }

        public async Task<ContentResult> GetByParamns(string auth, string display, string filterparams)
        {
            var result = await _storeBL.GetByParamns(auth, display, filterparams);
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }
        public async Task<ContentResult> GetByID( string id)
        {
            var result = await _storeBL.GetByID(new ObjectId(id));
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }
        public async Task<ContentResult> Add(string token, StoreItemCategoryModel entity)
        {
            var result = await _storeBL.Add(token, entity);
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }
        public async Task<ContentResult> Update(string token, StoreItemCategoryModel entity)
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