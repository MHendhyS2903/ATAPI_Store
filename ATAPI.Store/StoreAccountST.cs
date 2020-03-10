using ATAPI.Core.Helper;
using ATAPI.Core.Interface;
using ATAPI.Core.Model;
using ATAPI.Data;
using ATAPI.Store.Interface;
using ATAPI.Store.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATAPI.Store
{
    public class StoreAccountST : BaseDA, IAccountStore
    {
        private IMongoCollection<StoreAccountModel> _userCollection;
        private IMongoCollection<StoreModel> _storeCollection;
        private IMongoCollection<Core.Model.TransactionModel> _transactionCollection;
        private IMongoCollection<StoreItemModel> _storeItemCollection;
        private IMongoCollection<StoreOutletModel> _storeOutletCollection;
        private IAreaService _areaBL;

        public StoreAccountST(IServiceProvider serviceProvider)
        {
            _userCollection = DB.GetCollection<StoreAccountModel>("app_users");
            _storeCollection = DB.GetCollection<StoreModel>("store");
            _transactionCollection = DB.GetCollection<Core.Model.TransactionModel>("transactions");
            _storeItemCollection = DB.GetCollection<StoreItemModel>("store_item");

            _areaBL = (IAreaService)serviceProvider.GetService(typeof(IAreaService));
        }

        public StoreAccountST()
        {
            _userCollection = DB.GetCollection<StoreAccountModel>("app_users");
            _storeCollection = DB.GetCollection<StoreModel>("store");
            _transactionCollection = DB.GetCollection<Core.Model.TransactionModel>("transactions");
            _storeItemCollection = DB.GetCollection<StoreItemModel>("store_item");

        }

        public async Task<ResponseModel<long>> IsExists(StoreAccountModel entity)
        {
            var entityResult = new ResponseModel<long>();

            try
            {
                entityResult.Value = await _userCollection.Find(x => x.id == entity.id).CountDocumentsAsync();
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

        public ResponseModel<long> IsExistsTest(StoreAccountModel entity)
        {
            var entityResult = new ResponseModel<long>();

            try
            {
                entityResult.Value = _userCollection.Find(x => x.id == entity.id).CountDocuments();
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

        public async Task<ResponseModel<int>> Registration(StoreAccountModel entity)
        {
            var entityResult = new ResponseModel<int>();

            try
            {
                var rnd = new Random();
                entity.cellular = PhoneHelper.ValidPhone(entity.cellular);
                entity.status = "CREATED";
                entity.token = EncryptionHelper.Base64Encode(entity.password + entity.cellular + entity.email + rnd.Next(0, 9999).ToString());
                entity.created_at = DateTime.Now.ToUniversalTime();
                entity.updated_at = DateTime.Now.ToUniversalTime();

                if (entity.password != null)
                    entity.password = EncryptionHelper.Base64Encode(entity.password);

                var isRegistered = await IsExists(entity);
                if (isRegistered.Status == true && isRegistered.Value == 0)
                {
                    await _userCollection.InsertOneAsync(entity);
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

        public ResponseModel<int> RegistrationTest(StoreAccountModel entity)
        {
            var entityResult = new ResponseModel<int>();

            try
            {
                var rnd = new Random();
                entity.cellular = PhoneHelper.ValidPhone(entity.cellular);
                entity.status = "CREATED";
                entity.token = EncryptionHelper.Base64Encode(entity.password + entity.cellular + entity.email + rnd.Next(0, 9999).ToString());
                entity.created_at = DateTime.Now.ToUniversalTime();
                entity.updated_at = DateTime.Now.ToUniversalTime();

                if (entity.password != null)
                    entity.password = EncryptionHelper.Base64Encode(entity.password);

                var isRegistered = IsExistsTest(entity);
                if (isRegistered.Status == true && isRegistered.Value == 0)
                {
                    _userCollection.InsertOne(entity);
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

        public async Task<ResponseModel<StoreAccountModel>> Update(StoreAccountModel entity, string usertoken)
        {
            var entityResult = new ResponseModel<StoreAccountModel>();
            try
            {
                var itemdCart = await _userCollection.Find(x => x.token == usertoken).FirstOrDefaultAsync();
                if (itemdCart != null)
                {
                    if (entity.cellular == null)
                    {
                        var result = await _userCollection.UpdateOneAsync(
                        x => x.token == usertoken,
                        Builders<StoreAccountModel>.Update.Set(x => x.fullname, entity.fullname)
                        .Set(x => x.photo, entity.photo)
                        .Set(x => x.domicilecity, entity.domicilecity)
                        .Set(x => x.cellular, entity.cellular)
                        );

                        if (result.ModifiedCount > 0)
                        {
                            entityResult.Value = await _userCollection.Find(x => x.token == usertoken).FirstOrDefaultAsync();
                            entityResult.Status = true;
                        }
                        else
                        {
                            entityResult.Messages.Add(new ResponseMessageModel()
                            {
                                Type = ResponseMessageModel.MessageType.ERROR,
                                Title = "Error",
                                Message = "ERROR"
                            });
                        }
                    }
                    else 
                    {
                        var result = await _userCollection.UpdateOneAsync(
                        x => x.token == usertoken,
                        Builders<StoreAccountModel>.Update.Set(x => x.fullname, entity.fullname)
                        .Set(x => x.password, entity.password)
                        );

                        if (result.ModifiedCount > 0)
                        {
                            entityResult.Value = await _userCollection.Find(x => x.token == usertoken).FirstOrDefaultAsync();
                            entityResult.Status = true;
                        }
                        else
                        {
                            entityResult.Messages.Add(new ResponseMessageModel()
                            {
                                Type = ResponseMessageModel.MessageType.ERROR,
                                Title = "Error",
                                Message = "ERROR"
                            });
                        }
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

        public ResponseModel<StoreAccountModel> UpdateTest(StoreAccountModel entity, string usertoken)
        {
            var entityResult = new ResponseModel<StoreAccountModel>();
            try
            {
                var itemdCart = _userCollection.Find(x => x.token == usertoken).FirstOrDefault();
                if (itemdCart != null)
                {
                    if (entity.cellular == null)
                    {
                        var result = _userCollection.UpdateOne(
                        x => x.token == usertoken,
                        Builders<StoreAccountModel>.Update.Set(x => x.fullname, entity.fullname)
                        .Set(x => x.photo, entity.photo)
                        .Set(x => x.domicilecity, entity.domicilecity)
                        .Set(x => x.cellular, entity.cellular)
                        );

                        if (result.ModifiedCount > 0)
                        {
                            entityResult.Value = _userCollection.Find(x => x.token == usertoken).FirstOrDefault();
                            entityResult.Status = true;
                        }
                        else
                        {
                            entityResult.Messages.Add(new ResponseMessageModel()
                            {
                                Type = ResponseMessageModel.MessageType.ERROR,
                                Title = "Error",
                                Message = "ERROR"
                            });
                        }
                    }
                    else
                    {
                        var result = _userCollection.UpdateOne(
                        x => x.token == usertoken,
                        Builders<StoreAccountModel>.Update.Set(x => x.fullname, entity.fullname)
                        .Set(x => x.password, entity.password)
                        );

                        if (result.ModifiedCount > 0)
                        {
                            entityResult.Value = _userCollection.Find(x => x.token == usertoken).FirstOrDefault();
                            entityResult.Status = true;
                        }
                        else
                        {
                            entityResult.Messages.Add(new ResponseMessageModel()
                            {
                                Type = ResponseMessageModel.MessageType.ERROR,
                                Title = "Error",
                                Message = "ERROR"
                            });
                        }
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

        public async Task<ResponseModel<StoreAccountModel>> Login(string cellular, string password)
        {
            var entityResult = new ResponseModel<StoreAccountModel>();

            try
            {
                cellular = PhoneHelper.ValidPhone(cellular);

                var store = await _userCollection.Find(x => x.cellular == cellular).FirstOrDefaultAsync();

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

        public ResponseModel<StoreAccountModel> LoginTest(string cellular, string password)
        {
            var entityResult = new ResponseModel<StoreAccountModel>();

            try
            {
                cellular = PhoneHelper.ValidPhone(cellular);

                var store = _userCollection.Find(x => x.cellular == cellular).FirstOrDefault();

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

        public async Task<ResponseModel<StoreAccountModel>> GetByToken(string token, ObjectId id)
        {
            var entityResult = new ResponseModel<StoreAccountModel>();
            var store = await _userCollection.Find(x => x.token == token).FirstOrDefaultAsync();
            try
            {
                if (store != null)
                {
                    entityResult.Value = await _userCollection.Find(x => x.id == id).FirstOrDefaultAsync();
                    if (entityResult.Value != null)
                    {
                        entityResult.Status = true;
                    }
                    else
                    {
                        entityResult.Messages.Add(new ResponseMessageModel()
                        {
                            Type = ResponseMessageModel.MessageType.WARNING,
                            Title = "Restricted Access",
                            Message = "False!"
                        });
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
                    Title = "Null",
                    Message = ex.Message
                });
            }

            return entityResult;
        }

        public ResponseModel<StoreAccountModel> GetByTokenTest(string token, ObjectId id)
        {
            var entityResult = new ResponseModel<StoreAccountModel>();
            var store = _userCollection.Find(x => x.token == token).FirstOrDefault();
            try
            {
                if (store != null)
                {
                    entityResult.Value = _userCollection.Find(x => x.id == id).FirstOrDefault();
                    if (entityResult.Value != null)
                    {
                        entityResult.Status = true;
                    }
                    else
                    {
                        entityResult.Messages.Add(new ResponseMessageModel()
                        {
                            Type = ResponseMessageModel.MessageType.WARNING,
                            Title = "Restricted Access",
                            Message = "False!"
                        });
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
                    Title = "Null",
                    Message = ex.Message
                });
            }

            return entityResult;
        }

        public async Task<ResponseModel<IEnumerable<StoreAccountModel>>> GetByParams(string auth, string display, string filterparams)
        {
            var entityResult = new ResponseModel<IEnumerable<StoreAccountModel>>();

            try
            {
                var user = await _userCollection.Find(x => x.token == auth).FirstOrDefaultAsync();

                if (user != null)
                {
                    var filters = JsonConvert.DeserializeObject<List<FilterModel>>(filterparams);
                    var displayview = JsonConvert.DeserializeObject<DisplayViewModel>(display);

                    int startRow = (displayview.limit * displayview.page) - displayview.limit;

                    var queryfilter = QueryHelper.QueryFilterMDB<StoreAccountModel>(filters);
                    var filterbson = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(queryfilter);

                    var totalRows = await _userCollection.Find(queryfilter).CountDocumentsAsync();
                    entityResult.Messages.Add(new ResponseMessageModel()
                    {
                        Type = ResponseMessageModel.MessageType.SUCCESS,
                        Title = "TotalRows",
                        Message = totalRows.ToString()
                    });

                    entityResult.Value = await _userCollection.Find(queryfilter).Limit(displayview.limit).Skip(startRow).ToListAsync();
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

        public ResponseModel<IEnumerable<StoreAccountModel>> GetByParamsTest(string auth, string display, string filterparams)
        {
            var entityResult = new ResponseModel<IEnumerable<StoreAccountModel>>();

            try
            {
                var user = _userCollection.Find(x => x.token == auth).FirstOrDefault();

                if (user != null)
                {
                    var filters = JsonConvert.DeserializeObject<List<FilterModel>>(filterparams);
                    var displayview = JsonConvert.DeserializeObject<DisplayViewModel>(display);

                    int startRow = (displayview.limit * displayview.page) - displayview.limit;

                    var queryfilter = QueryHelper.QueryFilterMDB<StoreAccountModel>(filters);
                    var filterbson = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(queryfilter);

                    var totalRows = _userCollection.Find(queryfilter).CountDocuments();
                    entityResult.Messages.Add(new ResponseMessageModel()
                    {
                        Type = ResponseMessageModel.MessageType.SUCCESS,
                        Title = "TotalRows",
                        Message = totalRows.ToString()
                    });

                    entityResult.Value = _userCollection.Find(queryfilter).Limit(displayview.limit).Skip(startRow).ToList();
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
    }
}
