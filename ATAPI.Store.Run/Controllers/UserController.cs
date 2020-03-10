﻿using System;
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
    public class UserController : Controller
    {
        IAccountStore _storeBL;
        JsonSerializerSettings _jsonSettings;

        public UserController(IServiceProvider serviceProvider)
        {
            _jsonSettings = new JsonSerializerSettings();
            _jsonSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";

            _storeBL = (IAccountStore)serviceProvider.GetService(typeof(IAccountStore));
        }

        public async Task<ContentResult> IsExists(StoreAccountModel entity)
        {
            var result = await _storeBL.IsExists(entity);
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }

        public async Task<ContentResult> GetByParams(string auth, string display, string filterparams)
        {
            var result = await _storeBL.GetByParams(auth, display, filterparams);
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }

        public async Task<ContentResult> GetByToken(string token, string id)
        {
            var result = await _storeBL.GetByToken(token, new ObjectId(id));
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }

        public async Task<ContentResult> Registration(StoreAccountModel entity)
        {
            var result = await _storeBL.Registration(entity);
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }

        public async Task<ContentResult> Update(StoreAccountModel entity, string token)
        {
            var result = await _storeBL.Update(entity, token);
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }

        public async Task<ContentResult> Login(string cellular, string password)
        {
            var result = await _storeBL.Login(cellular, password);
            return new JsonStringModel(JsonConvert.SerializeObject(result, _jsonSettings));
        }



    }
}