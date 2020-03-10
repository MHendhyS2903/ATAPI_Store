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
    public class WishlistController : Controller
    {
        IWishlistStore _storeBL;
        JsonSerializerSettings _jsonSettings;

        public WishlistController(IServiceProvider serviceProvider)
        {
            _jsonSettings = new JsonSerializerSettings();
            _jsonSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";

            _storeBL = (IWishlistStore)serviceProvider.GetService(typeof(IWishlistStore));
        }


        public async Task<ContentResult> GetByToken(string usertoken)
        {
            var result = await _storeBL.GetByToken(usertoken);
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }

        public async Task<ContentResult> GetByParamns(string auth, string display, string filterparams)
        {
            var result = await _storeBL.GetByParamns(auth, display, filterparams);
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }

        public async Task<ContentResult> Add(string usertoken, ObjectId iditem, StoreWishlistModel entity)
        {
            var result = await _storeBL.Add(usertoken, iditem, entity);
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }

        public async Task<ContentResult> Delete(string id, string usertoken)
        {
            var result = await _storeBL.Delete(new ObjectId(id), usertoken);
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }
    }
}