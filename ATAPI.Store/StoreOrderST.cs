using ATAPI.Core.Helper;
using ATAPI.Core.Model;
using ATAPI.Data;
using ATAPI.Store.Interface;
using ATAPI.Store.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATAPI.Store
{
    public class StoreOrderST : BaseDA, IOrderStore
    {
        private IMongoCollection<StoreAccountModel> _userCollection;
        private IMongoCollection<StoreModel> _storeCollection;
        private IMongoCollection<Core.Model.TransactionModel> _transactionCollection;
        private IMongoCollection<StoreItemModel> _storeItemCollection;
        private IMongoCollection<StoreItemModel> _storeItemCategoryCollection;
        private IMongoCollection<StoreAppUsers> _storeAppUserCollection;
        private IMongoCollection<StoreOrderModel> _storeOrderCollection;

        public StoreOrderST()
        {
            _userCollection = DB.GetCollection<StoreAccountModel>("users");
            _storeCollection = DB.GetCollection<StoreModel>("store");
            _transactionCollection = DB.GetCollection<Core.Model.TransactionModel>("transactions");
            _storeItemCollection = DB.GetCollection<StoreItemModel>("items");
            _storeItemCategoryCollection = DB.GetCollection<StoreItemModel>("item_category");
            _storeAppUserCollection = DB.GetCollection<StoreAppUsers>("app_users");
            _storeOrderCollection = DB.GetCollection<StoreOrderModel>("order");
        }

        public async Task<ResponseModel<StoreOrderModel>> GetByID(ObjectId id)
        {
            var entityResult = new ResponseModel<StoreOrderModel>();
            try
            {
                entityResult.Value = await _storeOrderCollection.Find(x => x.id == id).FirstOrDefaultAsync();
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

        public ResponseModel<StoreOrderModel> GetByIDTest(ObjectId id)
        {
            var entityResult = new ResponseModel<StoreOrderModel>();
            try
            {
                entityResult.Value = _storeOrderCollection.Find(x => x.id == id).FirstOrDefault();
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

       

        public async Task<ResponseModel<StoreOrderModel>> GetByCurrentTransaction(string orderid)
        {
            var entityResult = new ResponseModel<StoreOrderModel>();
            try
            {
                var user = await _storeOrderCollection.Find(x => x.orderid == orderid).FirstOrDefaultAsync();
                if (user != null)
                {
                    entityResult.Value = await _storeOrderCollection.Find(x => x.orderid == orderid && (x.status == "ACTIVE" || x.status == "UNPAID")).FirstOrDefaultAsync();
                    if (entityResult.Value != null)
                    {
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

        public ResponseModel<StoreOrderModel> GetByCurrentTransactionTest(string orderid)
        {
            var entityResult = new ResponseModel<StoreOrderModel>();
            try
            {
                var user = _storeOrderCollection.Find(x => x.orderid == orderid).FirstOrDefault();
                if (user != null)
                {
                    entityResult.Value = _storeOrderCollection.Find(x => x.orderid == orderid && (x.status == "ACTIVE" || x.status == "UNPAID")).FirstOrDefault();
                    if (entityResult.Value != null)
                    {
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

        public async Task<ResponseModel<StoreOrderModel>> GetByActiveTransaction(string orderid)
        {
            var entityResult = new ResponseModel<StoreOrderModel>();
            try
            {
                entityResult.Value = await _storeOrderCollection.Find(x => x.status == "ACTIVE").FirstOrDefaultAsync();
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

        public ResponseModel<StoreOrderModel> GetByActiveTransactionTest(string orderid)
        {
            var entityResult = new ResponseModel<StoreOrderModel>();
            try
            {
                entityResult.Value = _storeOrderCollection.Find(x => x.status == "ACTIVE").FirstOrDefault();
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

        public async Task<ResponseModel<IEnumerable<StoreOrderModel>>> GetByUnpaidTransaction(string orderid)
        {
            var entityResult = new ResponseModel<IEnumerable<StoreOrderModel>>();
            try
            {
                var user = await _storeOrderCollection.Find(x => x.orderid == orderid).FirstOrDefaultAsync();
                if (user != null)
                {
                    entityResult.Value = await _storeOrderCollection.Find(x => x.orderid == orderid && x.status == "UNPAID").ToListAsync();
                    if (entityResult.Value != null)
                    {
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

        public ResponseModel<StoreOrderModel> GetByUnpaidTransactionTest(string orderid)
        {
            var entityResult = new ResponseModel<StoreOrderModel>();
            try
            {
                var user = _storeOrderCollection.Find(x => x.orderid == orderid).FirstOrDefault();
                if (user != null)
                {
                    entityResult.Value = _storeOrderCollection.Find(x => x.orderid == orderid && x.status == "UNPAID").FirstOrDefault();
                    if (entityResult.Value != null)
                    {
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
        public async Task<ResponseModel<StoreOrderModel>> GetByFinishedTransaction(ObjectId id)
        {
            var entityResult = new ResponseModel<StoreOrderModel>();
            try
            {
                entityResult.Value = await _storeOrderCollection.Find(x => x.status == "FINISH").FirstOrDefaultAsync();
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

        public ResponseModel<StoreOrderModel> GetByFinishedTransactionTest(ObjectId id)
        {
            var entityResult = new ResponseModel<StoreOrderModel>();
            try
            {
                entityResult.Value = _storeOrderCollection.Find(x => x.status == "FINISH").FirstOrDefault();
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
        public async Task<ResponseModel<StoreOrderModel>> GetByOrderId(string orderid)
        {
            var entityResult = new ResponseModel<StoreOrderModel>();
            try
            {
                entityResult.Value = await _storeOrderCollection.Find(x => x.orderid == orderid).FirstOrDefaultAsync();
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

        public ResponseModel<StoreOrderModel> GetByOrderIdTest(string orderid)
        {
            var entityResult = new ResponseModel<StoreOrderModel>();
            try
            {
                entityResult.Value = _storeOrderCollection.Find(x => x.orderid == orderid).FirstOrDefault();
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

       

        public async Task<ResponseModel<StoreOrderModel>> Add(StoreOrderModel entity)
        {
            var entityResult = new ResponseModel<StoreOrderModel>();

            try
            {
                if (entity.cart.Count > 0)
                {
                    var rnd = new Random();
                    entity.orderid = "AT-" + DateTime.Now.ToString("yyyyMMddHHmmss") + rnd.Next(0, 9999).ToString();
                    entity.created_at = DateTime.Now.ToUniversalTime();
                    entity.updated_at = DateTime.Now.ToUniversalTime();

                    foreach (var cart in entity.cart)
                    {
                        cart.subtotal = cart.GetTotal();
                        cart.created_at = DateTime.Now.ToUniversalTime();
                        cart.updated_at = DateTime.Now.ToUniversalTime();
                    }


                    //_storeOrderCollection.Find("{}").ToList();
                    //_storeOrderCollection.Find(new BsonDocument()).Limit;

                    entity.status = "UNPAID";
                    await _storeOrderCollection.InsertOneAsync(entity);
                    if (entity.id != new ObjectId())
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
                        Title = "Cart Empty",
                        Message = "Keranjang Belanja tidak boleh Kosong!"
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

        public ResponseModel<StoreOrderModel> AddTest(StoreOrderModel entity)
        {
            var entityResult = new ResponseModel<StoreOrderModel>();

            try
            {
                if (entity.cart.Count > 0)
                {
                    var rnd = new Random();
                    entity.orderid = "AT-" + DateTime.Now.ToString("yyyyMMddHHmmss") + rnd.Next(0, 9999).ToString();
                    entity.created_at = DateTime.Now.ToUniversalTime();
                    entity.updated_at = DateTime.Now.ToUniversalTime();

                    foreach (var cart in entity.cart)
                    {
                        cart.subtotal = cart.GetTotal();
                        cart.created_at = DateTime.Now.ToUniversalTime();
                        cart.updated_at = DateTime.Now.ToUniversalTime();
                    }


                    //_storeOrderCollection.Find("{}").ToList();
                    //_storeOrderCollection.Find(new BsonDocument()).Limit;

                    entity.status = "UNPAID";
                    _storeOrderCollection.InsertOne(entity);
                    if (entity.id != new ObjectId())
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
                        Title = "Cart Empty",
                        Message = "Keranjang Belanja tidak boleh Kosong!"
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
        public async Task<ResponseModel<StoreOrderModel>> Update(StoreOrderModel entity, string orderid, string token)
        {
            var entityResult = new ResponseModel<StoreOrderModel>();
            try
            {
                var dates = DateTime.Now.ToUniversalTime();

                var order = await _storeOrderCollection.Find(x => x.orderid == orderid).FirstOrDefaultAsync();
                var user = await _userCollection.Find(x => x.token == token).FirstOrDefaultAsync();

                if (user != null && order != null)
                {

                    var result = await _storeOrderCollection.UpdateOneAsync(x => x.orderid == entity.orderid,
                                Builders<StoreOrderModel>.Update.Set(x => x.status, "ACTIVE")
                                .Set(x => x.updated_at, dates)
                                );

                    if (result.ModifiedCount > 0)
                    {
                        entityResult.Value = await _storeOrderCollection.Find(x => x.orderid == orderid).FirstOrDefaultAsync();
                        entityResult.Status = true;
                    }
                }
                else
                {
                    entity.status = null;

                    var result = await _storeOrderCollection.UpdateOneAsync(x => x.orderid == entity.orderid,
                                Builders<StoreOrderModel>.Update.Set(x => x.status, "ACTIVE")
                                .Set(x => x.updated_at, dates)
                                );

                    if (result.ModifiedCount > 0)
                    {
                        entityResult.Value = await _storeOrderCollection.Find(x => x.orderid == orderid).FirstOrDefaultAsync();
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

        public ResponseModel<StoreOrderModel> UpdateTest(StoreOrderModel entity, string orderid, string token)
        {
            var entityResult = new ResponseModel<StoreOrderModel>();
            try
            {
                var dates = DateTime.Now.ToUniversalTime();

                var order = _storeOrderCollection.Find(x => x.orderid == orderid).FirstOrDefault();
                var user = _userCollection.Find(x => x.token == token).FirstOrDefault();

                if (user != null && order != null)
                {

                    var result = _storeOrderCollection.UpdateOne(x => x.orderid == entity.orderid,
                                Builders<StoreOrderModel>.Update.Set(x => x.status, "ACTIVE")
                                .Set(x => x.updated_at, dates)
                                );

                    if (result.ModifiedCount > 0)
                    {
                        entityResult.Value = _storeOrderCollection.Find(x => x.orderid == orderid).FirstOrDefault();
                        entityResult.Status = true;
                    }
                }
                else
                {
                    entity.status = null;

                    var result = _storeOrderCollection.UpdateOne(x => x.orderid == entity.orderid,
                                Builders<StoreOrderModel>.Update.Set(x => x.status, "ACTIVE")
                                .Set(x => x.updated_at, dates)
                                );

                    if (result.ModifiedCount > 0)
                    {
                        entityResult.Value = _storeOrderCollection.Find(x => x.orderid == orderid).FirstOrDefault();
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

        Task<ResponseModel<StoreOrderModel>> IOrderStore.GetByUnpaidTransaction(string orderid)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<StoreOrderModel>> GetByFinishedTransaction(string orderid)
        {
            throw new NotImplementedException();
        }

        public ResponseModel<StoreOrderModel> GetByFinishedTransactionTest(string orderid)
        {
            throw new NotImplementedException();
        }
    }
}
