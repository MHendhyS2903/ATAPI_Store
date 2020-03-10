using ATAPI.Store.Model;
using ATAPI.Store.Interface;
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

namespace ATAPI.StoreOutlet
{
    public class StoreOutletST : BaseDA
    {
        private IMongoCollection<Core.Model.UserModel> _userCollection;
        private IMongoCollection<StoreModel> _storeCollection;
        private IMongoCollection<Core.Model.TransactionModel> _transactionCollection;
        private IMongoCollection<StoreItemModel> _storeItemCollection;
        private IMongoCollection<StoreOutletModel> _storeOutletCollection;
        public StoreOutletST()
        {
            _userCollection = DB.GetCollection<Core.Model.UserModel>("users");
            _storeCollection = DB.GetCollection<StoreModel>("stores");
            _transactionCollection = DB.GetCollection<Core.Model.TransactionModel>("transactions");
            _storeItemCollection = DB.GetCollection<StoreItemModel>("items");
            _storeOutletCollection = DB.GetCollection<StoreOutletModel>("outlets");
        }

        public async Task<ResponseModel<IEnumerable<StoreOutletModel>>> GetByParamns(string auth, string display, string filterparams)
        {
            var entityResult = new ResponseModel<IEnumerable<StoreOutletModel>>();

            try
            {
                var user = await _userCollection.Find(x => x.token == auth).FirstOrDefaultAsync();

                if (user != null)
                {
                    var filters = JsonConvert.DeserializeObject<List<FilterModel>>(filterparams);
                    var displayview = JsonConvert.DeserializeObject<DisplayViewModel>(display);

                    int startRow = (displayview.limit * displayview.page) - displayview.limit;

                    var queryfilter = QueryHelper.QueryFilterMDB<StoreOutletModel>(filters);
                    var filterbson = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(queryfilter);

                    var totalRows = await _storeOutletCollection.Find(queryfilter).CountDocumentsAsync();
                    entityResult.Messages.Add(new ResponseMessageModel()
                    {
                        Type = ResponseMessageModel.MessageType.SUCCESS,
                        Title = "TotalRows",
                        Message = totalRows.ToString()
                    });

                    entityResult.Value = await _storeOutletCollection.Find(queryfilter).Limit(displayview.limit).Skip(startRow).ToListAsync();
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

        public async Task<ResponseModel<IEnumerable<StoreOutletModel>>> GetByToken(string token)
        {
            var entityResult = new ResponseModel<IEnumerable<StoreOutletModel>>();

            try
            {
                var store = await _storeCollection.Find(x => x.token == token).FirstOrDefaultAsync();
                if (store != null)
                {
                    entityResult.Value = await _storeOutletCollection.Find(x => x.idstore == store.id).ToListAsync();
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

        public ResponseModel<StoreOutletModel> GetByID(string token, ObjectId id)
        {
            var entityResult = new ResponseModel<StoreOutletModel>();
            var store = _storeCollection.Find(x => x.token == token).FirstOrDefault();
            try
            {
                    entityResult.Value = _storeOutletCollection.Find(x => x.id == id).FirstOrDefault();
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

        public async Task<ResponseModel<int>> Add(string token, StoreOutletModel entity)
        {
            var entityResult = new ResponseModel<int>();

            try
            {
                var store = await _storeCollection.Find(x => x.token == token).FirstOrDefaultAsync();
                if (store != null)
                {
                    entity.idstore = store.id;
                    entity.created_at = DateTime.Now.ToUniversalTime();
                    entity.updated_at = DateTime.Now.ToUniversalTime();

                    await _storeOutletCollection.InsertOneAsync(entity);
                    entityResult.Value = entity.id.Pid;
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

        public async Task<ResponseModel<long>> Update(string token, StoreOutletModel entity)
        {
            var entityResult = new ResponseModel<long>();

            try
            {
                var store = await _storeCollection.Find(x => x.token == token).FirstOrDefaultAsync();
                if (store != null)
                {
                    entity.updated_at = DateTime.Now.ToUniversalTime();
                    entity.idstore = store.id;

                    var updateResult = await _storeOutletCollection.UpdateOneAsync(
                        x => x.id == entity.id,
                        QueryHelper.QueryUpdateMDB<StoreOutletModel>(entity));

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

        public async Task<ResponseModel<long>> Delete(string token, string id)
        {
            var entityResult = new ResponseModel<long>();

            try
            {

                var store = await _storeCollection.Find(x => x.token == token).FirstOrDefaultAsync();
                if (store != null)
                {
                    entityResult.Value = _storeOutletCollection.DeleteOne(x => x.id == new ObjectId(id)).DeletedCount;
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
    }    
}
