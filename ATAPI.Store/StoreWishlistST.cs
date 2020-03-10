using ATAPI.Core.Helper;
using ATAPI.Core.Model;
using ATAPI.Data;
using ATAPI.Store.Model;
using ATAPI.Store.Interface;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATAPI.Store
{
    public class StoreWishlistST : BaseDA, IWishlistStore
    {
        private IMongoCollection<StoreAppUsers> _userCollection;
        private IMongoCollection<StoreModel> _storeCollection;
        private IMongoCollection<StoreItemModel> _storeItemCollection;
        private IMongoCollection<StoreWishlistModel> _storeWishlistCollection;

        public StoreWishlistST(IServiceProvider serviceProvider)
        {
            _userCollection = DB.GetCollection<StoreAppUsers>("app_users");
            _storeCollection = DB.GetCollection<StoreModel>("store");
            _storeItemCollection = DB.GetCollection<StoreItemModel>("items");
            _storeWishlistCollection = DB.GetCollection<StoreWishlistModel>("wishlist");
        }

        public StoreWishlistST()
        {
            _userCollection = DB.GetCollection<StoreAppUsers>("app_users");
            _storeCollection = DB.GetCollection<StoreModel>("store");
            _storeItemCollection = DB.GetCollection<StoreItemModel>("items");
            _storeWishlistCollection = DB.GetCollection<StoreWishlistModel>("wishlist");
        }

        public async Task<ResponseModel<IEnumerable<StoreWishlistModel>>> GetByToken(string usertoken)
        {
            var entityResult = new ResponseModel<IEnumerable<StoreWishlistModel>>();
            
            try
            {
                entityResult.Value = await _storeWishlistCollection.Find(x => x.usertoken == usertoken).ToListAsync();
                if (entityResult.Value != null)
                {
                    entityResult.Status = true;
                    entityResult.Messages.Add(new ResponseMessageModel()
                    {
                        Type = ResponseMessageModel.MessageType.WARNING,
                        Title = "Restricted Access",
                        Message = "False!"
                    });
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
            catch (Exception ex)
            {
                entityResult.Messages.Add(new ResponseMessageModel()
                {
                    Type = ResponseMessageModel.MessageType.ERROR,
                    Title = "Null",
                    Message = ex.Message + "S A L A H  N I H"
                });
            }
            return entityResult;
        }

        public ResponseModel<StoreWishlistModel> GetByTokenTest(string usertoken)
        {
            var entityResult = new ResponseModel<StoreWishlistModel>();
            try
            {
                entityResult.Value = _storeWishlistCollection.Find(x => x.usertoken == usertoken).FirstOrDefault();
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

        public async Task<ResponseModel<IEnumerable<StoreWishlistModel>>> GetByParamns(string auth, string display, string filterparams)
        {
            var entityResult = new ResponseModel<IEnumerable<StoreWishlistModel>>();

            try
            {
                    var filters = JsonConvert.DeserializeObject<List<FilterModel>>(filterparams);
                    var displayview = JsonConvert.DeserializeObject<DisplayViewModel>(display);

                    int startRow = (displayview.limit * displayview.page) - displayview.limit;

                    var queryfilter = QueryHelper.QueryFilterMDB<StoreWishlistModel>(filters);
                    var filterbson = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(queryfilter);

                    var totalRows = await _storeItemCollection.Find(queryfilter).CountDocumentsAsync();
                    entityResult.Messages.Add(new ResponseMessageModel()
                    {
                        Type = ResponseMessageModel.MessageType.SUCCESS,
                        Title = "TotalRows",
                        Message = totalRows.ToString()
                    });

                    entityResult.Value = await _storeWishlistCollection.Find(queryfilter).Limit(displayview.limit).Skip(startRow).ToListAsync();
                    entityResult.Status = true;
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

        public ResponseModel<IEnumerable<StoreWishlistModel>> GetByParamnsTest(string auth, string display, string filterparams)
        {
            var entityResult = new ResponseModel<IEnumerable<StoreWishlistModel>>();

            try
            {
                var filters = JsonConvert.DeserializeObject<List<FilterModel>>(filterparams);
                var displayview = JsonConvert.DeserializeObject<DisplayViewModel>(display);

                int startRow = (displayview.limit * displayview.page) - displayview.limit;

                var queryfilter = QueryHelper.QueryFilterMDB<StoreWishlistModel>(filters);
                var filterbson = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(queryfilter);

                var totalRows = _storeItemCollection.Find(queryfilter).CountDocuments();
                entityResult.Messages.Add(new ResponseMessageModel()
                {
                    Type = ResponseMessageModel.MessageType.SUCCESS,
                    Title = "TotalRows",
                    Message = totalRows.ToString()
                });

                entityResult.Value = _storeWishlistCollection.Find(queryfilter).Limit(displayview.limit).Skip(startRow).ToList();
                entityResult.Status = true;
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

        public async Task<ResponseModel<int>> Add(string usertoken, ObjectId iditem, StoreWishlistModel entity)
        {
            var entityResult = new ResponseModel<int>();
            try
            {
                var item = await _storeItemCollection.Find(x => x.id == iditem).ToListAsync();
                if (item != null)
                {
                    entity.usertoken = usertoken;
                    entity.iditem = iditem;

                    foreach (var items in item)
                    {
                        entity.idcategory = items.idcategory;
                        entity.idstore = items.idstore;
                        entity.title = items.title;
                        entity.status = items.status;
                        entity.description = items.description;
                        entity.price = items.price;
                        entity.photo = items.photo;
                        entity.created_at = items.created_at;
                        entity.updated_at = items.updated_at;
                    }

                    await _storeWishlistCollection.InsertOneAsync(entity);
                    entityResult.Value = entity.id.Pid;
                    entityResult.Status = true;
                }
                else
                {
                    entityResult.Messages.Add(new ResponseMessageModel()
                    {
                        Type = ResponseMessageModel.MessageType.ERROR,
                        Title = "Error",
                        Message = "Stock Empty"
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

        public ResponseModel<int> AddTest(string usertoken, ObjectId iditem, StoreWishlistModel entity)
        {
            var entityResult = new ResponseModel<int>();
            try
            {
                var item = _storeItemCollection.Find(x => x.id == iditem).ToList();
                if (item != null)
                {
                    entity.usertoken = usertoken;
                    entity.iditem = iditem;

                    foreach (var items in item)
                    {
                        entity.idcategory = items.idcategory;
                        entity.idstore = items.idstore;
                        entity.title = items.title;
                        entity.status = items.status;
                        entity.description = items.description;
                        entity.price = items.price;
                        entity.photo = items.photo;
                        entity.created_at = items.created_at;
                        entity.updated_at = items.updated_at;
                    }

                    _storeWishlistCollection.InsertOne(entity);
                    entityResult.Value = entity.id.Pid;
                    entityResult.Status = true;
                }
                else
                {
                    entityResult.Messages.Add(new ResponseMessageModel()
                    {
                        Type = ResponseMessageModel.MessageType.ERROR,
                        Title = "Error",
                        Message = "Stock Empty"
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

        public async Task<ResponseModel<StoreWishlistModel>> Delete(ObjectId id, string usertoken)
        {
            var entityResult = new ResponseModel<StoreWishlistModel>();
            var entity = new StoreWishlistModel();
                try
                {
                    await _storeWishlistCollection.DeleteOneAsync(x => x.id == id && x.usertoken == usertoken);
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

        public ResponseModel<StoreWishlistModel> DeleteTest(ObjectId id, string usertoken)
        {
            var entityResult = new ResponseModel<StoreWishlistModel>();
            var entity = new StoreWishlistModel();
            try
            {
                _storeWishlistCollection.DeleteOne(x => x.id == id && x.usertoken == usertoken);
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
    }
}
