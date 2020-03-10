using ATAPI.Core.Helper;
using ATAPI.Core.Model;
using ATAPI.Data;
using ATAPI.Store.Interface;
using ATAPI.Store.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATAPI.Store
{
    public class StoreCartST : BaseDA, ICartStore
    {
        private IMongoCollection<StoreAppUsers> _userCollection;
        private IMongoCollection<StoreModel> _storeCollection;
        private IMongoCollection<StoreItemModel> _storeItemCollection;
        private IMongoCollection<StoreCartModel> _storeCartCollection;

        public StoreCartST(IServiceProvider serviceProvider)
        {
            _userCollection = DB.GetCollection<StoreAppUsers>("users");
            _storeCollection = DB.GetCollection<StoreModel>("store");
            _storeItemCollection = DB.GetCollection<StoreItemModel>("items");
            _storeCartCollection = DB.GetCollection<StoreCartModel>("cart");
        }

        public StoreCartST()
        {
            _userCollection = DB.GetCollection<StoreAppUsers>("users");
            _storeCollection = DB.GetCollection<StoreModel>("store");
            _storeItemCollection = DB.GetCollection<StoreItemModel>("items");
            _storeCartCollection = DB.GetCollection<StoreCartModel>("cart");
        }

        public async Task<ResponseModel<IEnumerable<StoreCartModel>>> GetByToken(string usertoken)
        {
            var entityResult = new ResponseModel<IEnumerable<StoreCartModel>>();
            try
            {
                entityResult.Value = await _storeCartCollection.Find(x => x.usertoken == usertoken).ToListAsync();
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

        public ResponseModel<StoreCartModel> GetByTokenTest(string usertoken)
        {
            var entityResult = new ResponseModel<StoreCartModel>();
            try
            {
                entityResult.Value = _storeCartCollection.Find(x => x.usertoken == usertoken).FirstOrDefault();
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

        public async Task<ResponseModel<int>> Add(string usertoken, ObjectId iditem, StoreCartModel entity)
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

                    entity.subtotal = entity.GetTotal();

                    await _storeCartCollection.InsertOneAsync(entity);
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

        public ResponseModel<int> AddTest(string usertoken, ObjectId iditem, StoreCartModel entity)
        {
            var entityResult = new ResponseModel<int>();
            try
            {
                var item = _storeItemCollection.Find(x => x.id == iditem).ToList();
                if (item != null)
                {
                    entity.usertoken = usertoken;
                    entity.iditem = iditem;

                    foreach (var items in item) {
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

                    entity.subtotal = entity.GetTotal();

                    _storeCartCollection.InsertOne(entity);
                    entityResult.Value = entity.id.Pid;
                    entityResult.Status = true;
                }
                else {
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

        public async Task<ResponseModel<StoreCartModel>> Update(StoreCartModel entity, string usertoken, string iditem)
        {
            var entityResult = new ResponseModel<StoreCartModel>();
            var item = new ObjectId(iditem);
            try
            {
                var itemdCart = await _storeCartCollection.Find(x => x.usertoken == usertoken && x.iditem == item).FirstOrDefaultAsync();
                if (itemdCart != null)
                {

                    var result = await _storeCartCollection.UpdateOneAsync(
                        x => x.usertoken == usertoken && x.iditem == item,
                        Builders<StoreCartModel>.Update.Set(x => x.iditem, item)
                        .Set(x => x.qty, entity.qty)
                        );


                    if (result.ModifiedCount > 0)
                    {
                        entityResult.Value = await _storeCartCollection.Find(x => x.usertoken == usertoken && x.iditem == item).FirstOrDefaultAsync();
                        entityResult.Status = true;
                    }
                }
                else
                {
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

        public ResponseModel<StoreCartModel> UpdateTest(StoreCartModel entity, string usertoken, string iditem)
        {
            var entityResult = new ResponseModel<StoreCartModel>();
            var item = new ObjectId(iditem);
            try
            {
                var itemdCart = _storeCartCollection.Find(x => x.usertoken == usertoken && x.iditem == item).FirstOrDefault();
                if (itemdCart != null)
                {

                    var result = _storeCartCollection.UpdateOne(
                        x => x.usertoken == usertoken && x.iditem == item,
                        Builders<StoreCartModel>.Update.Set(x => x.iditem, item)
                        .Set(x => x.qty, entity.qty)
                        );


                    if (result.ModifiedCount > 0)
                    {
                        entityResult.Value = _storeCartCollection.Find(x => x.usertoken == usertoken && x.iditem == item).FirstOrDefault();
                        entityResult.Status = true;
                    }
                }
                else
                {
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

        public async Task<ResponseModel<StoreCartModel>> Delete(ObjectId id, string usertoken)
        {
            var entityResult = new ResponseModel<StoreCartModel>();
            var entity = new StoreCartModel();
            try
            {
                await _storeCartCollection.DeleteOneAsync(x => x.id == id && x.usertoken == usertoken);
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

        public ResponseModel<StoreCartModel> DeleteTest(ObjectId id, string usertoken)
        {
            var entityResult = new ResponseModel<StoreCartModel>();
            var entity = new StoreCartModel();
            try
            {
                _storeCartCollection.DeleteOne(x => x.id == id && x.usertoken == usertoken);
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
