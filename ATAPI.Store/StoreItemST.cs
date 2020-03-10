using ATAPI.Core.Helper;
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
    public class StoreItemST : BaseDA, IItemService
    {
        private IMongoCollection<Core.Model.UserModel> _userCollection;
        private IMongoCollection<StoreModel> _storeCollection;
        private IMongoCollection<Core.Model.TransactionModel> _transactionCollection;
        private IMongoCollection<StoreItemModel> _storeItemCollection;
        private IMongoCollection<StoreOutletModel> _storeOutletCollection;

        public StoreItemST()
        {
            _userCollection = DB.GetCollection<Core.Model.UserModel>("users");
            _storeCollection = DB.GetCollection<StoreModel>("store");
            _transactionCollection = DB.GetCollection<Core.Model.TransactionModel>("transactions");
            _storeItemCollection = DB.GetCollection<StoreItemModel>("items");
            _storeOutletCollection = DB.GetCollection<StoreOutletModel>("store_outlets");
        }

        public async Task<ResponseModel<IEnumerable<StoreItemModel>>> GetByParamns(string auth, string display, string filterparams)
        {
            var entityResult = new ResponseModel<IEnumerable<StoreItemModel>>();

            try
            {
                var user = await _userCollection.Find(x => x.token == auth).FirstOrDefaultAsync();

                if (user != null)
                {
                    var filters = JsonConvert.DeserializeObject<List<FilterModel>>(filterparams);
                    var displayview = JsonConvert.DeserializeObject<DisplayViewModel>(display);

                    int startRow = (displayview.limit * displayview.page) - displayview.limit;

                    var queryfilter = QueryHelper.QueryFilterMDB<StoreItemModel>(filters);
                    var filterbson = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(queryfilter);

                    var totalRows = await _storeItemCollection.Find(queryfilter).CountDocumentsAsync();
                    entityResult.Messages.Add(new ResponseMessageModel()
                    {
                        Type = ResponseMessageModel.MessageType.SUCCESS,
                        Title = "TotalRows",
                        Message = totalRows.ToString()
                    });

                    entityResult.Value = _storeItemCollection.Find(queryfilter).Limit(displayview.limit).Skip(startRow).ToList();
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

        public ResponseModel<IEnumerable<StoreItemModel>> GetByParamnsTest(string auth, string display, string filterparams)
        {
            var entityResult = new ResponseModel<IEnumerable<StoreItemModel>>();

            try
            {
                var user = _userCollection.Find(x => x.token == auth).FirstOrDefault();

                if (user != null)
                {
                    var filters = JsonConvert.DeserializeObject<List<FilterModel>>(filterparams);
                    var displayview = JsonConvert.DeserializeObject<DisplayViewModel>(display);

                    int startRow = (displayview.limit * displayview.page) - displayview.limit;

                    var queryfilter = QueryHelper.QueryFilterMDB<StoreItemModel>(filters);
                    var filterbson = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(queryfilter);

                    var totalRows = _storeItemCollection.Find(queryfilter).CountDocuments();
                    entityResult.Messages.Add(new ResponseMessageModel()
                    {
                        Type = ResponseMessageModel.MessageType.SUCCESS,
                        Title = "TotalRows",
                        Message = totalRows.ToString()
                    });

                    entityResult.Value = _storeItemCollection.Find(queryfilter).Limit(displayview.limit).Skip(startRow).ToList();
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

       

        public async Task<ResponseModel<StoreItemModel>> GetByID(ObjectId id)
        {
            var entityResult = new ResponseModel<StoreItemModel>();
            try
            {
                entityResult.Value = await _storeItemCollection.Find(x => x.id == id).FirstOrDefaultAsync();
                if (entityResult.Value != null)
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

        public ResponseModel<StoreItemModel> GetByIDTest(ObjectId id)
        {
            var entityResult = new ResponseModel<StoreItemModel>();
            try
            {
                entityResult.Value = _storeItemCollection.Find(x => x.id == id).FirstOrDefault();
                if (entityResult.Value != null)
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

        public async Task<ResponseModel<IEnumerable<StoreItemModel>>> GetByStore(ObjectId storeid)
        {
            var entityResult = new ResponseModel<IEnumerable<StoreItemModel>>();

            try
            {
                entityResult.Value = await _storeItemCollection.Find(x => x.idstore == storeid).ToListAsync();
                if (entityResult.Value != null)
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

        public ResponseModel<IEnumerable<StoreItemModel>> GetByStoreTest(ObjectId storeid)
        {
            var entityResult = new ResponseModel<IEnumerable<StoreItemModel>>();

            try
            {
                entityResult.Value = _storeItemCollection.Find(x => x.idstore == storeid).ToList();
                if (entityResult.Value != null)
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

        public async Task<ResponseModel<IEnumerable<StoreItemModel>>> GetByCategory(ObjectId categoryid)
        {
            var entityResult = new ResponseModel<IEnumerable<StoreItemModel>>();

            try
            {
                entityResult.Value = await _storeItemCollection.Find(x => x.idcategory == categoryid).ToListAsync();
                if (entityResult.Value != null)
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

        public ResponseModel<IEnumerable<StoreItemModel>> GetByCategoryTest(ObjectId categoryid)
        {
            var entityResult = new ResponseModel<IEnumerable<StoreItemModel>>();

            try
            {
                entityResult.Value = _storeItemCollection.Find(x => x.idcategory == categoryid).ToList();
                if (entityResult.Value != null)
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

        public async Task<ResponseModel<int>> Add(string token, StoreItemModel entity)
        {
            var entityResult = new ResponseModel<int>();

            try
            {
                var store = await _storeCollection.Find(x => x.token == token).FirstOrDefaultAsync();
                if (store != null)
                {
                    entity.created_at = DateTime.Now.ToUniversalTime();
                    entity.updated_at = DateTime.Now.ToUniversalTime();
                    entity.status = "CREATED";

                    _storeItemCollection.InsertOne(entity);
                    entityResult.Status = true;
                    //if (entity.id.Pid > 0)
                    //{
                    //    entityResult.Status = true;
                    //}
                    //else
                    //{
                    //    entityResult.Messages.Add(new ResponseMessageModel()
                    //    {
                    //        Type = ResponseMessageModel.MessageType.WARNING,
                    //        Title = "Action Failed",
                    //        Message = "Gagal melakukan perubahan data!"
                    //    });
                    //}
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

        public ResponseModel<int> AddTest(string token, StoreItemModel entity)
        {
            var entityResult = new ResponseModel<int>();

            try
            {
                var store = _storeCollection.Find(x => x.token == token).FirstOrDefault();
                if (store != null)
                {
                    entity.created_at = DateTime.Now.ToUniversalTime();
                    entity.updated_at = DateTime.Now.ToUniversalTime();
                    entity.status = "CREATED";

                    _storeItemCollection.InsertOne(entity);
                    entityResult.Status = true;
                    //if (entity.id.Pid > 0)
                    //{
                    //    entityResult.Status = true;
                    //}
                    //else
                    //{
                    //    entityResult.Messages.Add(new ResponseMessageModel()
                    //    {
                    //        Type = ResponseMessageModel.MessageType.WARNING,
                    //        Title = "Action Failed",
                    //        Message = "Gagal melakukan perubahan data!"
                    //    });
                    //}
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

        public async Task<ResponseModel<long>> Update(string token, StoreItemModel entity)
        {
            var entityResult = new ResponseModel<long>();

            try
            {
                var item = await _storeItemCollection.Find(x => x.id == entity.id).FirstOrDefaultAsync();
                var store = await _storeCollection.Find(x => x.token == token).FirstOrDefaultAsync();
                if (store != null && item != null)
                {
                    
                    if (entity.idstore == new ObjectId())
                    {
                        entity.idstore = item.idstore;
                    }

                    if (entity.idcategory == new ObjectId())
                    {
                        entity.idcategory = item.idcategory;
                    }
                    entity.updated_at = DateTime.Now.ToUniversalTime();

                        var updateResult = await _storeItemCollection.UpdateOneAsync(
                            x => x.id == entity.id,
                            QueryHelper.QueryUpdateMDB<StoreItemModel>(entity)
                            );

                        entityResult.Value = updateResult.ModifiedCount;
                        entityResult.Status = true;

                        if (entityResult.Value > 0)
                        {
                            entityResult.Status = true;
                        }
                        else
                        {
                            entityResult.Messages.Add(new ResponseMessageModel()
                            {
                                Type = ResponseMessageModel.MessageType.WARNING,
                                Title = "Action Failed",
                                Message = "Gagal melakukan perubahan data!"
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
                    Title = "Error",
                    Message = ex.Message
                });
            }

            return entityResult;
        }

        public ResponseModel<long> UpdateTest(string token, StoreItemModel entity)
        {
            var entityResult = new ResponseModel<long>();

            try
            {
                var item = _storeItemCollection.Find(x => x.id == entity.id).FirstOrDefault();
                var store = _storeCollection.Find(x => x.token == token).FirstOrDefault();
                if (store != null && item != null)
                {

                    if (entity.idstore == new ObjectId())
                    {
                        entity.idstore = item.idstore;
                    }

                    if (entity.idcategory == new ObjectId())
                    {
                        entity.idcategory = item.idcategory;
                    }
                    entity.updated_at = DateTime.Now.ToUniversalTime();

                    var updateResult = _storeItemCollection.UpdateOne(
                        x => x.id == entity.id,
                        QueryHelper.QueryUpdateMDB<StoreItemModel>(entity)
                        );

                    entityResult.Value = updateResult.ModifiedCount;
                    entityResult.Status = true;

                    if (entityResult.Value > 0)
                    {
                        entityResult.Status = true;
                    }
                    else
                    {
                        entityResult.Messages.Add(new ResponseMessageModel()
                        {
                            Type = ResponseMessageModel.MessageType.WARNING,
                            Title = "Action Failed",
                            Message = "Gagal melakukan perubahan data!"
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
                    Title = "Error",
                    Message = ex.Message
                });
            }

            return entityResult;
        }

        public async Task<ResponseModel<StoreItemModel>> Delete(string token, ObjectId id)
        {
            var entityResult = new ResponseModel<StoreItemModel>();
            var entity = new StoreItemModel();

            var item = await _storeItemCollection.Find(x => x.id == entity.id).FirstOrDefaultAsync();
            var store = await _storeCollection.Find(x => x.token == token).FirstOrDefaultAsync();
            if (store != null && item != null)
            {
                try
                {
                    if (entity.idstore == new ObjectId())
                    {
                        entity.idstore = item.idstore;
                    }

                    if (entity.idcategory == new ObjectId())
                    {
                        entity.idcategory = item.idcategory;
                    }

                    entity.updated_at = DateTime.Now.ToUniversalTime();
                    entity.status = "DELETED";

                    var result = await _storeItemCollection.UpdateOneAsync(
                        x => x.id == id,
                        QueryHelper.QueryUpdateMDB<StoreItemModel>(entity)
                        );

                    if (result.ModifiedCount > 0)
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
            }

            return entityResult;
        }

        public ResponseModel<StoreItemModel> DeleteTest(string token, ObjectId id)
        {
            var entityResult = new ResponseModel<StoreItemModel>();
            var entity = new StoreItemModel();

            var item = _storeItemCollection.Find(x => x.id == entity.id).FirstOrDefault();
            var store = _storeCollection.Find(x => x.token == token).FirstOrDefault();
            if (store != null && item != null)
            {
                try
                {
                    if (entity.idstore == new ObjectId())
                    {
                        entity.idstore = item.idstore;
                    }

                    if (entity.idcategory == new ObjectId())
                    {
                        entity.idcategory = item.idcategory;
                    }

                    entity.updated_at = DateTime.Now.ToUniversalTime();
                    entity.status = "DELETED";

                    var result = _storeItemCollection.UpdateOne(
                        x => x.id == id,
                        QueryHelper.QueryUpdateMDB<StoreItemModel>(entity)
                        );

                    if (result.ModifiedCount > 0)
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
            }

            return entityResult;
        }
    }
}
