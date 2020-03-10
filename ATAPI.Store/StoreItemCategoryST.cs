using ATAPI.Core.Helper;
using ATAPI.Core.Interface;
using ATAPI.Core.Model;
using ATAPI.Store.Interface;
using ATAPI.Store.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATAPI.Data
{
    public class StoreItemCategoryST : BaseDA, IItemCategoryStore
    {
        private IMongoCollection<StoreAccountModel> _userCollection;
        private IMongoCollection<StoreModel> _storeCollection;
        private IMongoCollection<Core.Model.TransactionModel> _transactionCollection;
        private IMongoCollection<StoreItemModel> _storeItemCollection;
        private IMongoCollection<StoreOutletModel> _storeOutletCollection;
        private IMongoCollection<StoreItemCategoryModel> _storeItemCategoryCollection;
        private IAreaService _areaBL;

        public StoreItemCategoryST()
        {
            _userCollection = DB.GetCollection<StoreAccountModel>("users");
            _storeCollection = DB.GetCollection<StoreModel>("stores");
            _transactionCollection = DB.GetCollection<Core.Model.TransactionModel>("transactions");
            _storeItemCollection = DB.GetCollection<StoreItemModel>("items");
            _storeOutletCollection = DB.GetCollection<StoreOutletModel>("outlets");
            _storeItemCategoryCollection = DB.GetCollection<StoreItemCategoryModel>("item_category");
        }

        public async Task<ResponseModel<IEnumerable<StoreItemCategoryModel>>> GetByParamns(string auth, string display, string filterparams)
        {
            var entityResult = new ResponseModel<IEnumerable<StoreItemCategoryModel>>();

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

                    var totalRows = await _storeItemCategoryCollection.Find(queryfilter).CountDocumentsAsync();
                    entityResult.Messages.Add(new ResponseMessageModel()
                    {
                        Type = ResponseMessageModel.MessageType.SUCCESS,
                        Title = "TotalRows",
                        Message = totalRows.ToString()
                    });

                    entityResult.Value = await _storeItemCategoryCollection.Find(queryfilter).Limit(displayview.limit).Skip(startRow).ToListAsync(); ;
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
        public ResponseModel<IEnumerable<StoreItemCategoryModel>> GetByParamnsTest(string auth, string display, string filterparams)
        {
            var entityResult = new ResponseModel<IEnumerable<StoreItemCategoryModel>>();

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

                    var totalRows = _storeItemCategoryCollection.Find(queryfilter).CountDocuments();
                    entityResult.Messages.Add(new ResponseMessageModel()
                    {
                        Type = ResponseMessageModel.MessageType.SUCCESS,
                        Title = "TotalRows",
                        Message = totalRows.ToString()
                    });

                    entityResult.Value = _storeItemCategoryCollection.Find(queryfilter).Limit(displayview.limit).Skip(startRow).ToList(); ;
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

        public async Task<ResponseModel<StoreItemCategoryModel>> GetByID(ObjectId id)
        {
            var entityResult = new ResponseModel<StoreItemCategoryModel>();
            try
            {
                entityResult.Value = await _storeItemCategoryCollection.Find(x => x.id == id).FirstOrDefaultAsync();
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

        public ResponseModel<StoreItemCategoryModel> GetByIDTest(ObjectId id)
        {
            var entityResult = new ResponseModel<StoreItemCategoryModel>();
            try
            {
                entityResult.Value = _storeItemCategoryCollection.Find(x => x.id == id).FirstOrDefault();
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

        public async Task<ResponseModel<StoreItemCategoryModel>> Add(string token, StoreItemCategoryModel entity)
        {
            var entityResult = new ResponseModel<StoreItemCategoryModel>();

            var filterAdm = await _userCollection.Find(x => x.token == token).CountDocumentsAsync();
            if (filterAdm > 0)
            {
                try
                {
                    entity.created_at = DateTime.Now.ToUniversalTime();
                    entity.updated_at = DateTime.Now.ToUniversalTime();

                    await _storeItemCategoryCollection.InsertOneAsync(entity);

                    entityResult.Value = entity;
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

        public ResponseModel<StoreItemCategoryModel> AddTest(string token, StoreItemCategoryModel entity)
        {
            var entityResult = new ResponseModel<StoreItemCategoryModel>();

            var filterAdm = _userCollection.Find(x => x.token == token).CountDocuments();
            if (filterAdm > 0)
            {
                try
                {
                    entity.created_at = DateTime.Now.ToUniversalTime();
                    entity.updated_at = DateTime.Now.ToUniversalTime();

                    _storeItemCategoryCollection.InsertOne(entity);

                    entityResult.Value = entity;
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

        public async Task<ResponseModel<long>> Update(string token, StoreItemCategoryModel entity)
        {
            var entityResult = new ResponseModel<long>();

            var filterAdm = await _userCollection.Find(x => x.token == token).CountDocumentsAsync();
            if (filterAdm > 0)
            {
                try
                {
                    entity.updated_at = DateTime.Now.ToUniversalTime();

                    var updateResult = await _storeItemCategoryCollection.UpdateOneAsync(
                        x => x.id == entity.id,
                        QueryHelper.QueryUpdateMDB<StoreItemCategoryModel>(entity));

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

        public ResponseModel<long> UpdateTest(string token, StoreItemCategoryModel entity)
        {
            var entityResult = new ResponseModel<long>();

            var filterAdm = _userCollection.Find(x => x.token == token).CountDocuments();
            if (filterAdm > 0)
            {
                try
                {
                    entity.updated_at = DateTime.Now.ToUniversalTime();

                    var updateResult = _storeItemCategoryCollection.UpdateOne(
                        x => x.id == entity.id,
                        QueryHelper.QueryUpdateMDB<StoreItemCategoryModel>(entity));

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

        public async Task<ResponseModel<StoreItemCategoryModel>> Delete(string token, ObjectId id)
        {
            var entityResult = new ResponseModel<StoreItemCategoryModel>();
            var entity = new StoreItemCategoryModel();
            var filterAdm = await _userCollection.Find(x => x.token == token).CountDocumentsAsync();
            if (filterAdm > 0)
            {
                try
                {
                    entity.updated_at = DateTime.Now.ToUniversalTime();

                    var result = await _storeItemCategoryCollection.DeleteOneAsync(x => x.id == id);

                    if (result.DeletedCount > 0)
                    {
                        entityResult.Value = entity;
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

        public ResponseModel<StoreItemCategoryModel> DeleteTest(string token, ObjectId id)
        {
            var entityResult = new ResponseModel<StoreItemCategoryModel>();
            var entity = new StoreItemCategoryModel();
            var filterAdm = _userCollection.Find(x => x.token == token).CountDocuments();
            if (filterAdm > 0)
            {
                try
                {
                    entity.updated_at = DateTime.Now.ToUniversalTime();

                    var result = _storeItemCategoryCollection.DeleteOne(x => x.id == id);

                    if (result.DeletedCount > 0)
                    {
                        entityResult.Value = entity;
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

        //public async Task<ResponseModel<long>> Delete(string id)
        //{
        //    var entityResult = new ResponseModel<long>();

        //    try
        //    {
        //            entityResult.Value = _storeItemCategoryCollection.DeleteOne(x => x.id == new ObjectId(id)).DeletedCount;
        //            entityResult.Status = true;

        //            if (entityResult.Value > 0)
        //            {
        //                entityResult.Status = true;
        //            }
        //            else
        //            {
        //                entityResult.Messages.Add(new ResponseMessageModel()
        //                {
        //                    Type = ResponseMessageModel.MessageType.WARNING,
        //                    Title = "Action Failed",
        //                    Message = "Gagal melakukan perubahan data!"
        //                });
        //            }
        //    }
        //    catch (Exception ex)
        //    {
        //        entityResult.Messages.Add(new ResponseMessageModel()
        //        {
        //            Type = ResponseMessageModel.MessageType.ERROR,
        //            Title = "Error",
        //            Message = ex.Message
        //        });
        //    }

        //    return entityResult;
        //}
    }
}
