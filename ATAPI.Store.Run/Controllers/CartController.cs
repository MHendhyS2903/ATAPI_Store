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
    public class CartController : Controller
    {
        ICartStore _storeBL;
        JsonSerializerSettings _jsonSettings;

        public CartController(IServiceProvider serviceProvider)
        {
            _jsonSettings = new JsonSerializerSettings();
            _jsonSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";

            _storeBL = (ICartStore)serviceProvider.GetService(typeof(ICartStore));
        }

        public async Task<ContentResult> GetByToken(string usertoken)
        {
            var result = await _storeBL.GetByToken(usertoken);
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }

        public async Task<ContentResult> Add(string usertoken, ObjectId iditem, StoreCartModel entity)
        {
            var result = await _storeBL.Add(usertoken, iditem, entity);
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }

        public async Task<ContentResult> Update(StoreCartModel entity, string usertoken, string iditem)
        {
            var result = await _storeBL.Update(entity, usertoken, iditem);
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }

        public async Task<ContentResult> Delete(string id, string usertoken)
        {
            var result = await _storeBL.Delete(new ObjectId(id), usertoken);
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }
    }

}
