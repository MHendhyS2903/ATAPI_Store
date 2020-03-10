using ATAPI.Store.Model;
using ATAPI.Data;
using ATAPI.Core.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using Newtonsoft.Json;
using ATAPI.Core.Helper;
using ATAPI.Core.Interface;
using ATAPI.Store.Interface;

namespace ATAPI.Store
{
    public class StoreST : BaseDA, IRegistStore
    {
        private IMongoCollection<Core.Model.UserModel> _userCollection;
        private IMongoCollection<StoreModel> _storeCollection;
        private IMongoCollection<Core.Model.TransactionModel> _transactionCollection;
        private IMongoCollection<StoreItemModel> _storeItemCollection;
        private IMongoCollection<StoreOutletModel> _storeOutletCollection;
        private IAreaService _areaBL;

        public StoreST(IServiceProvider serviceProvider)
        {
            _userCollection = DB.GetCollection<Core.Model.UserModel>("users");
            _storeCollection = DB.GetCollection<StoreModel>("store");
            _transactionCollection = DB.GetCollection<Core.Model.TransactionModel>("transactions");
            _storeItemCollection = DB.GetCollection<StoreItemModel>("store_item");

            _areaBL = (IAreaService)serviceProvider.GetService(typeof(IAreaService));
        }

        public StoreST()
        {
            _userCollection = DB.GetCollection<Core.Model.UserModel>("users");
            _storeCollection = DB.GetCollection<StoreModel>("store");
            _transactionCollection = DB.GetCollection<Core.Model.TransactionModel>("transactions");
            _storeItemCollection = DB.GetCollection<StoreItemModel>("store_item");
        }

        public async Task<ResponseModel<long>> IsExists(StoreModel entity)
        {
            var entityResult = new ResponseModel<long>();

            try
            {
                entityResult.Value = await _storeCollection.Find(x => x.email == entity.email || x.hp1 == entity.hp1).CountDocumentsAsync();
                entityResult.Status = true;
            }
            catch (Exception ex)
            {
                entityResult.Messages.Add(new ResponseMessageModel()
                {
                    Type = ResponseMessageModel.MessageType.ERROR,
                    Title = "Error",
                    Message = ex.Message
                });
            }

            return entityResult;
        }

        public ResponseModel<long> IsExistsTest(StoreModel entity)
        {
            var entityResult = new ResponseModel<long>();

            try
            {
                entityResult.Value = _storeCollection.Find(x => x.email == entity.email || x.hp1 == entity.hp1).CountDocuments();
                entityResult.Status = true;
            }
            catch (Exception ex)
            {
                entityResult.Messages.Add(new ResponseMessageModel()
                {
                    Type = ResponseMessageModel.MessageType.ERROR,
                    Title = "Error",
                    Message = ex.Message
                });
            }

            return entityResult;
        }

        //public async Task<ResponseModel<IEnumerable<StoreModel>>> GetByParamns(string auth, string display, string filterparams)
        //{
        //    var entityResult = new ResponseModel<IEnumerable<StoreModel>>();

        //    try
        //    {
        //        var user = await _userCollection.Find(x => x.token == auth).FirstOrDefaultAsync();

        //        if (user != null)
        //        {
        //            var filters = JsonConvert.DeserializeObject<List<FilterModel>>(filterparams);
        //            var displayview = JsonConvert.DeserializeObject<DisplayViewModel>(display);

        //            int startRow = (displayview.limit * displayview.page) - displayview.limit;

        //            var queryfilter = QueryHelper.QueryFilterMDB<StoreOutletModel>(filters);
        //            var filterbson = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(queryfilter);

        //            var totalRows = await _storeCollection.Find(queryfilter).CountDocumentsAsync();
        //            entityResult.Messages.Add(new ResponseMessageModel()
        //            {
        //                Type = ResponseMessageModel.MessageType.SUCCESS,
        //                Title = "TotalRows",
        //                Message = totalRows.ToString()
        //            });

        //            entityResult.Value = await _storeCollection.Find(queryfilter).Limit(displayview.limit).Skip(startRow).ToListAsync();
        //            entityResult.Status = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        entityResult.Messages.Add(new ResponseMessageModel()
        //        {
        //            Type = ResponseMessageModel.MessageType.ERROR,
        //            Title = "Error",
        //            Message = ex.Message + " - " + ex.StackTrace
        //        });
        //    }

        //    return entityResult;
        //}

        public async Task<ResponseModel<StoreModel>> GetByID(string token, ObjectId id)
        {
            var entityResult = new ResponseModel<StoreModel>();

            try
            {
                var store = await _storeCollection.Find(x => x.token == token).FirstOrDefaultAsync();
                if (store != null)
                {
                    entityResult.Value = await _storeCollection.Find(x => x.id == id).FirstOrDefaultAsync();
                    if (entityResult.Value != null)
                    {
                        entityResult.Status = true;
                    }
                }
                else
                {
                    entityResult.Messages.Add(new ResponseMessageModel()
                    {
                        Type = ResponseMessageModel.MessageType.WARNING,
                        Title = "Restricted Access",
                        Message = "Anda tidak bisa melakukan aksi ini!"
                    });
                }
            }
            catch (Exception ex)
            {
                entityResult.Messages.Add(new ResponseMessageModel()
                {
                    Type = ResponseMessageModel.MessageType.ERROR,
                    Title = "Error",
                    Message = ex.Message
                });
            }

            return entityResult;
        }

        public ResponseModel<StoreModel> GetByIDTest(string token, ObjectId id)
        {
            var entityResult = new ResponseModel<StoreModel>();

            try
            {
                var store = _storeCollection.Find(x => x.token == token).FirstOrDefault();
                if (store != null)
                {
                    entityResult.Value = _storeCollection.Find(x => x.id == id).FirstOrDefault();
                    if (entityResult.Value != null)
                    {
                        entityResult.Status = true;
                    }
                }
                else
                {
                    entityResult.Messages.Add(new ResponseMessageModel()
                    {
                        Type = ResponseMessageModel.MessageType.WARNING,
                        Title = "Restricted Access",
                        Message = "Anda tidak bisa melakukan aksi ini!"
                    });
                }
            }
            catch (Exception ex)
            {
                entityResult.Messages.Add(new ResponseMessageModel()
                {
                    Type = ResponseMessageModel.MessageType.ERROR,
                    Title = "Error",
                    Message = ex.Message
                });
            }

            return entityResult;
        }

        public async Task<ResponseModel<IEnumerable<StoreModel>>> GetByParams(string auth, string display, string filterparams)
        {
            var entityResult = new ResponseModel<IEnumerable<StoreModel>>();

            try
            {
                var user = await _userCollection.Find(x => x.token == auth).FirstOrDefaultAsync();

                if (user != null)
                {
                    var filters = JsonConvert.DeserializeObject<List<FilterModel>>(filterparams);
                    var displayview = JsonConvert.DeserializeObject<DisplayViewModel>(display);

                    int startRow = (displayview.limit * displayview.page) - displayview.limit;

                    var queryfilter = QueryHelper.QueryFilterMDB<StoreModel>(filters);
                    var filterbson = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(queryfilter);

                    var totalRows = await _storeCollection.Find(queryfilter).CountDocumentsAsync();
                    entityResult.Messages.Add(new ResponseMessageModel()
                    {
                        Type = ResponseMessageModel.MessageType.SUCCESS,
                        Title = "TotalRows",
                        Message = totalRows.ToString()
                    });

                    //var result = new List<StoreInfoModel>();
                    var stores = await _storeCollection.Find(queryfilter).SortByDescending(x => x.created_at).Limit(displayview.limit).Skip(startRow).ToListAsync();

                    entityResult.Value = stores;
                    entityResult.Status = true;
                }
            }
            catch (Exception ex)
            {
                entityResult.Messages.Add(new ResponseMessageModel()
                {
                    Type = ResponseMessageModel.MessageType.ERROR,
                    Title = "Error",
                    Message = ex.Message + " - " + ex.StackTrace
                });
            }

            return entityResult;
        }

        public ResponseModel<IEnumerable<StoreModel>> GetByParamsTest(string auth, string display, string filterparams)
        {
            var entityResult = new ResponseModel<IEnumerable<StoreModel>>();

            try
            {
                var user = _storeCollection.Find(x => x.token == auth).FirstOrDefault();

                if (user != null)
                {
                    var filters = JsonConvert.DeserializeObject<List<FilterModel>>(filterparams);
                    var displayview = JsonConvert.DeserializeObject<DisplayViewModel>(display);

                    int startRow = (displayview.limit * displayview.page) - displayview.limit;

                    var queryfilter = QueryHelper.QueryFilterMDB<StoreModel>(filters);
                    var filterbson = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(queryfilter);

                    var totalRows = _storeCollection.Find(queryfilter).CountDocuments();
                    entityResult.Messages.Add(new ResponseMessageModel()
                    {
                        Type = ResponseMessageModel.MessageType.SUCCESS,
                        Title = "TotalRows",
                        Message = totalRows.ToString()
                    });

                    //var result = new List<StoreInfoModel>();
                    var stores = _storeCollection.Find(queryfilter).SortByDescending(x => x.created_at).Limit(displayview.limit).Skip(startRow).ToList();

                    entityResult.Value = stores;
                    entityResult.Status = true;
                }
            }
            catch (Exception ex)
            {
                entityResult.Messages.Add(new ResponseMessageModel()
                {
                    Type = ResponseMessageModel.MessageType.ERROR,
                    Title = "Error",
                    Message = ex.Message + " - " + ex.StackTrace
                });
            }

            return entityResult;
        }

        public async Task<ResponseModel<StoreModel>> GetByToken(string token, ObjectId id)
        {
            var entityResult = new ResponseModel<StoreModel>();

            try
            {
                var store = await _storeCollection.Find(x => x.token == token).FirstOrDefaultAsync();
                if (store != null)
                {
                    entityResult.Status = true;
                }
            }
            catch (Exception ex)
            {
                entityResult.Messages.Add(new ResponseMessageModel()
                {
                    Type = ResponseMessageModel.MessageType.ERROR,
                    Title = "Error",
                    Message = ex.Message
                });
            }

            return entityResult;
        }

        public ResponseModel<StoreModel> GetByTokenTest(string token, ObjectId id)
        {
            var entityResult = new ResponseModel<StoreModel>();

            try
            {
                var store = _storeCollection.Find(x => x.token == token).FirstOrDefault();
                if (store != null)
                {
                    entityResult.Status = true;
                }
            }
            catch (Exception ex)
            {
                entityResult.Messages.Add(new ResponseMessageModel()
                {
                    Type = ResponseMessageModel.MessageType.ERROR,
                    Title = "Error",
                    Message = ex.Message
                });
            }

            return entityResult;
        }

        public async Task<ResponseModel<int>> Registration(StoreModel entity)
        {
            var entityResult = new ResponseModel<int>();

            try
            {
                var rnd = new Random();
                entity.hp1 = PhoneHelper.ValidPhone(entity.hp1);
                entity.status = "CREATED";
                entity.token = EncryptionHelper.Base64Encode(entity.password + entity.hp1 + entity.email+rnd.Next(0,9999).ToString());
                entity.created_at = DateTime.Now.ToUniversalTime();
                entity.updated_at = DateTime.Now.ToUniversalTime();

                if (entity.password != null)
                    entity.password = EncryptionHelper.Base64Encode(entity.password);

                var isRegistered = await IsExists(entity);
                if (isRegistered.Status == true && isRegistered.Value == 0)
                {
                    await _storeCollection.InsertOneAsync(entity);
                    entityResult.Value = entity.id.Pid;
                    entityResult.Status = true;
                }
            }
            catch (Exception ex)
            {
                entityResult.Messages.Add(new ResponseMessageModel()
                {
                    Type = ResponseMessageModel.MessageType.ERROR,
                    Title = "Error",
                    Message = ex.Message
                });
            }

            return entityResult;
        }

        public ResponseModel<int> RegistrationTest(StoreModel entity)
        {
            var entityResult = new ResponseModel<int>();

            try
            {
                var rnd = new Random();
                entity.hp1 = PhoneHelper.ValidPhone(entity.hp1);
                entity.status = "CREATED";
                entity.token = EncryptionHelper.Base64Encode(entity.password + entity.hp1 + entity.email + rnd.Next(0, 9999).ToString());
                entity.created_at = DateTime.Now.ToUniversalTime();
                entity.updated_at = DateTime.Now.ToUniversalTime();

                if (entity.password != null)
                    entity.password = EncryptionHelper.Base64Encode(entity.password);

                var isRegistered = IsExistsTest(entity);
                if (isRegistered.Status == true && isRegistered.Value == 0)
                {
                    _storeCollection.InsertOne(entity);
                    entityResult.Value = entity.id.Pid;
                    entityResult.Status = true;
                }
            }
            catch (Exception ex)
            {
                entityResult.Messages.Add(new ResponseMessageModel()
                {
                    Type = ResponseMessageModel.MessageType.ERROR,
                    Title = "Error",
                    Message = ex.Message
                });
            }

            return entityResult;
        }

        public async Task<ResponseModel<StoreModel>> Update(StoreModel entity, string token)
        {
            var entityResult = new ResponseModel<StoreModel>();
            try
            {
                entity.hp1 = PhoneHelper.ValidPhone(entity.hp1);
                entity.updated_at = DateTime.Now.ToUniversalTime();

                var filterAdm = await _userCollection.Find(x => x.token == token).CountDocumentsAsync();
                if (filterAdm > 0)
                {
                    var result = await _storeCollection.UpdateOneAsync(
                    x => x.token == entity.token,
                    QueryHelper.QueryUpdateMDB<StoreModel>(entity)
                    );
                    if (result.ModifiedCount > 0)
                    {
                        entityResult.Value = await _storeCollection.Find(x => x.token == entity.token).FirstOrDefaultAsync();
                        entityResult.Status = true;
                    }
                }
                else
                {
                    entity.status = null;
                    var result = await _storeCollection.UpdateOneAsync(
                    x => x.token == entity.token,
                    QueryHelper.QueryUpdateMDB<StoreModel>(entity)
                    );
                    if (result.ModifiedCount > 0)
                    {
                        entityResult.Value = await _storeCollection.Find(x => x.token == entity.token).FirstOrDefaultAsync();
                        entityResult.Status = true;
                    }
                }
            }
            catch (Exception ex)
            {
                entityResult.Messages.Add(new ResponseMessageModel()
                {
                    Type = ResponseMessageModel.MessageType.ERROR,
                    Title = "Error",
                    Message = ex.Message
                });
            }

            return entityResult;
        }

        public ResponseModel<StoreModel> UpdateTest(StoreModel entity, string token )
        {
            var entityResult = new ResponseModel<StoreModel>();
            try
            {
                entity.hp1 = PhoneHelper.ValidPhone(entity.hp1);
                entity.updated_at = DateTime.Now.ToUniversalTime();

                var filterAdm = _userCollection.Find(x=>x.token == token).CountDocuments();
                if (filterAdm > 0)
                {
                    var result = _storeCollection.UpdateOne(
                    x => x.token == entity.token,
                    QueryHelper.QueryUpdateMDB<StoreModel>(entity)
                    );
                    if (result.ModifiedCount > 0)
                    {
                        entityResult.Value = _storeCollection.Find(x => x.token == entity.token).FirstOrDefault();
                        entityResult.Status = true;
                    }
                }else {
                    entity.status = null;
                    var result = _storeCollection.UpdateOne(
                    x => x.token == entity.token,
                    QueryHelper.QueryUpdateMDB<StoreModel>(entity)
                    );
                    if (result.ModifiedCount > 0)
                    {
                        entityResult.Value = _storeCollection.Find(x => x.token == entity.token).FirstOrDefault();
                        entityResult.Status = true;
                    }
                }
            }
            catch (Exception ex)
            {
                entityResult.Messages.Add(new ResponseMessageModel()
                {
                    Type = ResponseMessageModel.MessageType.ERROR,
                    Title = "Error",
                    Message = ex.Message
                });
            }

            return entityResult;
        }

        public async Task<ResponseModel<StoreModel>> Delete(string token, ObjectId id)
        {
            var entityResult = new ResponseModel<StoreModel>();
            var entity = new StoreModel();
            entity.status = "DELETED";

            var filterAdm = await _userCollection.Find(x => x.token == token).CountDocumentsAsync();
            if (filterAdm > 0)
            {
                try
                {
                    entity.hp1 = PhoneHelper.ValidPhone(entity.hp1);
                    entity.updated_at = DateTime.Now.ToUniversalTime();

                    var result = await _storeCollection.UpdateOneAsync(
                        x => x.id == id,
                        QueryHelper.QueryUpdateMDB<StoreModel>(entity)
                        );

                    if (result.ModifiedCount > 0)
                    {
                        entityResult.Value = await _storeCollection.Find(x => x.id == id).FirstOrDefaultAsync();
                        entityResult.Status = true;
                    }
                }
                catch (Exception ex)
                {
                    entityResult.Messages.Add(new ResponseMessageModel()
                    {
                        Type = ResponseMessageModel.MessageType.ERROR,
                        Title = "Error",
                        Message = ex.Message
                    });
                }
            }
            else
            {
                entityResult.Messages.Add(new ResponseMessageModel()
                {
                    Type = ResponseMessageModel.MessageType.WARNING,
                    Title = "Unautorized",
                    Message = "Anda tidak memiliki wewenang untuk melakukan tindakan ini"
                });
            }
            return entityResult;
        }

        public ResponseModel<StoreModel> DeleteTest(string token, ObjectId id)
        {
            var entityResult = new ResponseModel<StoreModel>();
            var entity = new StoreModel();
            entity.status = "DELETED";

            var filterAdm = _userCollection.Find(x => x.token == token).CountDocuments();
            if (filterAdm > 0)
            {
                try
                {
                    entity.hp1 = PhoneHelper.ValidPhone(entity.hp1);
                    entity.updated_at = DateTime.Now.ToUniversalTime();

                    var result = _storeCollection.UpdateOne(
                        x => x.id == id,
                        QueryHelper.QueryUpdateMDB<StoreModel>(entity)
                        );

                    if (result.ModifiedCount > 0)
                    {
                        entityResult.Value = _storeCollection.Find(x => x.id == id).FirstOrDefault();
                        entityResult.Status = true;
                    }
                }
                catch (Exception ex)
                {
                    entityResult.Messages.Add(new ResponseMessageModel()
                    {
                        Type = ResponseMessageModel.MessageType.ERROR,
                        Title = "Error",
                        Message = ex.Message
                    });
                }
            }
            else 
            {
                entityResult.Messages.Add(new ResponseMessageModel()
                {
                    Type = ResponseMessageModel.MessageType.WARNING,
                    Title = "Unautorized",
                    Message = "Anda tidak memiliki wewenang untuk melakukan tindakan ini"
                });
            }
            return entityResult;
        }

        public async Task<ResponseModel<StoreModel>> Login(string cellular, string password)
        {
            var entityResult = new ResponseModel<StoreModel>();

            try
            {
                cellular = PhoneHelper.ValidPhone(cellular);

                var store = await _storeCollection.Find(x => x.hp1 == cellular).FirstOrDefaultAsync();

                var pwdserver = EncryptionHelper.Base64Decode(store.password);
                if (pwdserver == password)
                {
                    entityResult.Value = store;
                    entityResult.Status = true;
                }
                else if (String.IsNullOrEmpty(store.password))
                {
                    entityResult.Messages.Add(new ResponseMessageModel()
                    {
                        Type = ResponseMessageModel.MessageType.ERROR,
                        Title = "Error",
                        Message = "Failed Login"
                    });
                }

            }
            catch (Exception ex)
            {
                entityResult.Messages.Add(new ResponseMessageModel()
                {
                    Type = ResponseMessageModel.MessageType.ERROR,
                    Title = "Error",
                    Message = ex.Message
                });
            }

            return entityResult;
        }


        public ResponseModel<StoreModel> LoginTest(string cellular, string password)
        {
            var entityResult = new ResponseModel<StoreModel>();

            try
            {
                cellular = PhoneHelper.ValidPhone(cellular);

                var store = _storeCollection.Find(x => x.hp1 == cellular).FirstOrDefault();

                var pwdserver = EncryptionHelper.Base64Decode(store.password);
                if (pwdserver == password)
                {
                    entityResult.Value = store;
                    entityResult.Status = true;
                }
                else if (String.IsNullOrEmpty(store.password))
                {
                    entityResult.Messages.Add(new ResponseMessageModel()
                    {
                        Type = ResponseMessageModel.MessageType.ERROR,
                        Title = "Error",
                        Message = "Failed Login"
                    });
                }

            }
            catch (Exception ex)
            {
                entityResult.Messages.Add(new ResponseMessageModel()
                {
                    Type = ResponseMessageModel.MessageType.ERROR,
                    Title = "Error",
                    Message = ex.Message
                });
            }

            return entityResult;
        }



        /*public async Task<ResponseModel<LoginModel>> HasAccount(string cellular)
        {
            var entityResult = new ResponseModel<LoginModel>();

            try
            {
                cellular = PhoneHelper.ValidPhone(cellular);
                var store = await _storeCollection.Find(x => x.hp1 == cellular).FirstOrDefaultAsync();
                if (store != null)
                {
                    entityResult.Value = new LoginModel()
                    {
                        cellular = store.hp1,
                        haspassword = (!String.IsNullOrEmpty(store.password) ? true : false)
                    };

                    entityResult.Status = true;
                }
            }
            catch (Exception ex)
            {
                entityResult.Messages.Add(new ResponseMessageModel()
                {
                    Type = ResponseMessageModel.MessageType.ERROR,
                    Title = "Error",
                    Message = ex.Message
                });
            }

            return entityResult;
        }

        public async Task<ResponseModel<long>> ResetPassword(StoreModel entity)
        {
            var entityResult = new ResponseModel<long>();

            try
            {
                var account = await HasAccount(entity.hp1);
                if (account.Status)
                {
                    entity.hp1 = PhoneHelper.ValidPhone(entity.hp1);
                    entity.password = EncryptionHelper.Base64Encode(entity.password);

                    entityResult.Value = 0;
                    entityResult.Status = true;
                }
            }
            catch (Exception ex)
            {
                entityResult.Messages.Add(new ResponseMessageModel()
                {
                    Type = ResponseMessageModel.MessageType.ERROR,
                    Title = "Error",
                    Message = ex.Message
                });
            }

            return entityResult;
        }*/
    }
}
