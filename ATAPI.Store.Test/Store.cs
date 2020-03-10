using ATAPI.Data;
using ATAPI.Store.Model;
using ATAPI.StoreOutlet;
using MongoDB.Bson;
using NUnit.Framework;
using System.Collections.Generic;

namespace ATAPI.Store.Test
{
    public class Store
    {
        private StoreAccountST _account;
        private StoreItemST _item;
        private StoreItemCategoryST _category;
        private StoreOrderST _order;
        private StoreST _regist;
        private StoreAccountST _user;
        private StoreWishlistST _wishlist;
        private StoreCartST _cart;

        [SetUp]
        public void Setup()
        {
            _account = new StoreAccountST();
            _item = new StoreItemST();
            _category = new StoreItemCategoryST();
            _order = new StoreOrderST();
            _regist = new StoreST();
            _user = new StoreAccountST();
            _wishlist= new StoreWishlistST();
            _cart= new StoreCartST();
        }

        //Store Item Category//////////////////////////////////////
        [Test]
        public void TestAddItemCategory()
        {
            string token = "";
            var entity = new StoreItemCategoryModel();
            entity.title = "Test2";
            entity.icon = "Test2";
            entity.description = "Test2";


            var result = _category.AddTest(token, entity);

            Assert.Pass();
        }

        [Test]
        public void TestUpdateItemCategory()
        {
            string token = "asjkldjlkajskldui212khkjdasd";
            var entity = new StoreItemCategoryModel();
            entity.id = new ObjectId("5ddb4a0bc7e4cd392c4a3314");
            entity.icon = "updated";
            entity.description = "updated";
            entity.title = "updated";
            var result = _category.Update(token, entity);

            Assert.AreEqual(result.Status, true);
        }

        [Test]
        public void TestGetByParamItemCategory()
        {
            //string token = "";
            //ObjectId id = new ObjectId("");

            ////var result = _category.GetByParamns(token, id);

            //Assert.Pass();
        }

        [Test]
        public void TestItemCategoryGetById()
        {
            ObjectId id = new ObjectId("5ddb4a0bc7e4cd392c4a3314");
            var result = _category.GetByID(id);

            Assert.AreEqual(result.Status, true);
        }

        [Test]
        public void TestDeleteItemCategory()
        {
            ObjectId id = new ObjectId("5dcd1d9a99abdc2478e75a52");
            string token = "asjkldjlkajskldui212khkjdasd";
            var result = _category.Delete(token, id);

            Assert.AreEqual(result.Status, true);
        }
        //END


        //Store Item//////////////////////////////////////////
        [Test]
        public void TestAddItem() 
        {
            string token = "dGVzdDEyMzYyODgxMjczOTg3ZGVwb2tAZ21haWwuY29tNzE299==";
            var entity = new StoreItemModel();
            entity.title = "test";
            entity.description = "test";
            entity.price = 500;
            entity.photo = "test";
            entity.idcategory = new ObjectId("5ddb4a0bc7e4cd392c4a3314") ;
            entity.idstore = new ObjectId("5ddb524c3a512b1b8c5fa44b");


            var result = _item.Add(token, entity);

            Assert.Pass();
        }

        [Test]
        public void TestUpdateItem() 
        {
            string token = "dGVzdDEyMzYyODgxMjczOTg3ZGVwb2tAZ21haWwuY29tNzE299==";
            
            var entity = new StoreItemModel();
            entity.id = new ObjectId("5ddb56ce5693ad2cb03dbe6c");
            entity.description = "updated";
            entity.title = "updated";
            var result = _item.Update(token, entity);

            Assert.AreEqual(result.Status, true);
        }
        [Test]
        public void TestGetByParamsAccount()
        {
            string auth = "dGVzdDEyMzYyODU5MjE3MDIwMjZtLmhlbmRoeS5zQGdtYWlsLmNvbTQyOTE=";
            string display = "{\"limit\":20,\"page\":1,\"field\":\"id\",\"direction\":\"asc\"}";
            string filterparam = "[{\"field\":\"cellular\",\"op\":\"eq\",\"value\":\"085921702026\"}]";
            var result = _user.GetByParamsTest(auth, display, filterparam);

            Assert.AreEqual(result.Status, true);
        }

        [Test]
        public void TestDeleteItem() 
        {
            string token = "dGVzdDEyMzYyODgxMjczOTg3ZGVwb2tAZ21haWwuY29tNzE299==";
            ObjectId id = new ObjectId("5ddb56ce5693ad2cb03dbe6c");
            var result = _item.Delete(token, id);

            Assert.AreEqual(result.Status, true);
        }

        [Test]
        public void TestGetByIdItem()
        {
            ObjectId id = new ObjectId("5dd4a253d2eb8417409480c0");
            var result = _item.GetByID(id);

            Assert.AreEqual(result.Status, true);
        }

        [Test]
        public void TestGetByStoreItem()
        {
            ObjectId id = new ObjectId("5dd4a253d2eb8417409480c0");
            var result = _item.GetByStore(id);

            Assert.AreEqual(result.Status, true);
        }

        [Test]
        public void TestGetByCategoryItem()
        {
            ObjectId id = new ObjectId("5dd4a253d2eb8417409480c0");
            var result = _item.GetByCategory(id);

            Assert.AreEqual(result.Status, true);
        }
        //END


        //Store//////////////////////////////////////////////
        [Test]
        public void TestStoreUpdate()
        {
            string token = "asjkldjlkajskldui212khkjdasd";
            var entity = new StoreModel();
            entity.hp1 = "0881273111";
            entity.nik = "99999999999";
            entity.status = "TEST UPDATE";
            entity.token = "dGVzdDEyMzYyODgxMjczOTg3ZGVwb2tAZ21haWwuY29tNzE2OQ==";
            var result = _regist.Update(entity, token);

            Assert.AreEqual(result.Status, true);
        }

        [Test]
        public void TestStoreDelete()
        {
            ObjectId id = new ObjectId("5dd4ad72686e361e8cee6ef6");
            string token = "asjkldjlkajskldui212khkjdasd";
            var result = _regist.Delete(token, id);

            Assert.AreEqual(result.Status, true);
        }

        [Test]
        public void TestStoreGetById()
        {
            string token = "";
            ObjectId id = new ObjectId("5dd6adb5477a893ef0eceeae");
            var result = _regist.GetByID(token, id);

            Assert.AreEqual(result.Status, true);
        }

        [Test]
        public void TestStoreGetByParams()
        {
            string auth = "asjkldjlkajskldui212khkjdasd";
            string display = "{\"limit\":20,\"page\":1,\"field\":\"id\",\"direction\":\"asc\"}";
            string filterparam = "[{\"field\":\"status\",\"op\":\"eq\",\"value\":\"CREATED\"}]";
            var result = _regist.GetByParamsTest(auth, display, filterparam);

            Assert.AreEqual(result.Status, true);
        }

        [Test]
        public void TestStoreByToken()
        {
            string token ="dGVzdDEyMzYyODgxMjczOTg3ZGVwb2tAZ21haWwuY29tNzE2OQ==";
            var result = _regist.GetByToken(token, string.Empty);

            Assert.AreEqual(result.Status, true);
        }

        [Test]
        public void TestRegistAccounte()
        {
            var entity = new StoreAccountModel();
            entity.cellular = "085921702026";
            entity.balance = "Test";
            entity.dokuid = "Test";
            entity.domicilecity = 1;
            entity.email = "m.hendhy.s@gmail.com";
            entity.fbtoken = "Test";
            entity.fullname = "Test";
            entity.idgoogle = "Test";
            entity.lat = 1;
            entity.lng = 1;
            entity.otpcode = "Test";
            entity.password = "test123";
            entity.photo = "Test";
            entity.reference = "Test";


            var result = _user.RegistrationTest(entity);

            Assert.Pass();
        }

        [Test]
        public void TestLoginStore()
        {
            string cellular = "62881273987";
            string password = "test123";


            var result = _regist.Login(cellular, password);

            Assert.Pass();
        }
        //END


        //Store Order////////////////////////////////
        [Test]
        public void TestOrderGetById()
        {
            ObjectId id = new ObjectId("5dd6adb5477a893ef0eceeae");
            var result = _order.GetByID(id);

            Assert.AreEqual(result.Status, true);
        }

        [Test]
        public void TestOrderGetByOrderId()
        {
            string orderid = "5dd4a253d2eb8417409480c0";
            var result = _order.GetByOrderId(orderid);

            Assert.AreEqual(result.Status, true);
        }

       /* [Test]
        public void TestOrderAdd()
        {
            var entityCart = new StoreCartModel();
            string storeid = "5dd4ad72686e361e8cee6ef6";
            string userid = "5dd4e758931a27475c85488d";

            var entity = new StoreOrderModel();

            ObjectId itemid = new ObjectId("5dcbbd3bca8611348cf3930f");

            entityCart.qty = 4;
            entity.cart.Add(entityCart);

            entity.latfrom = 12321312;
            entity.latto = 12321312;
            entity.lngfrom = 12321312;
            entity.lngto = 12321312;

            entity.distance = 50;
            entity.destination = "Test Destination";

            entity.reason = "Test Beli";
            entity.price = 123000;


            var result = _order.Add( storeid, userid, itemid, entity);

            Assert.Pass();
        }*/

        [Test]
        public void TestOrderUpdate()
        {
            string orderid = "AT-201911269838";
            string token = "asjkldjlkajskldui212khkjdasd";
            /*string itemid = "5dcbbd3bca8611348cf3930f";*/

            var entity = new StoreOrderModel();
            var entityCart = new StoreCartModel();

            entity.latfrom = 44444444;
            entity.latto = 444444;
            entity.lngfrom = 44444444;
            entity.lngto = 444444;

            entity.status = "UPDATED";

            entity.distance = 333;
            entity.destination = "Test Update Destination";

            entityCart.qty = 400;

            entity.cart.Add(entityCart);

            entity.reason = "Test Update Beli";
            entity.price = 1111;

            var result = _order.Update(entity, orderid, token);
            Assert.AreEqual(result.Status, true);
            }

        [Test]
        public void TestCartUpdate()
        {
            string iditem = "5dcbbd3bca8611348cf3930f";
            string usertoken = "MDg1Nzg2NTE4MDg4";

            var entity = new StoreCartModel();

            entity.qty = 3;

            var result = _cart.UpdateTest(entity, usertoken, iditem);
            Assert.AreEqual(result.Status, true);
        }

        //[Test]
        //public void TestOrderGetByCurrentTransaction()
        //{
        //    ObjectId iduser = new ObjectId();
        //    var result = _order.GetByCurrentTransaction(iduser);

        //    Assert.AreEqual(result.Status, true);
        //}

        [Test]
        public void TestAddWishlist()
        {
            string usertoken = "MDg1Nzg2NTE4MDg4";
            ObjectId iditem = new ObjectId("5e2fb60c830b792db0a54584");
            var entity = new StoreWishlistModel();

            var result = _wishlist.AddTest(usertoken, iditem, entity);
            Assert.Pass();
        }

        [Test]
        public void TestAddCart()
        {
            string usertoken = "MDg1Nzg2NTE4MDg4";
            ObjectId iditem = new ObjectId("5dcbbd3bca8611348cf3930f");
            var entity = new StoreCartModel();
            entity.notes = "TESTING";
            entity.price = 5000;
            entity.qty = 2;

            var result = _cart.AddTest(usertoken, iditem, entity);
            Assert.Pass();
        }

        [Test]
        public void TestGetByTokenWishlist()
        {
            string usertoken = "MDg1Nzg2NTE4MDg4";
            var result = _wishlist.GetByTokenTest(usertoken);

            Assert.AreEqual(result.Status, true);
        }

        //[Test]
        //public void TestOrderGetByActiveTransaction()
        //{
        //    var entity = new StoreOrderModel();
        //    entity.status = "ACTIVE";
        //    var result = _order.GetByActiveTransaction(entity);

        //    Assert.AreEqual(result.Status, true);
        //}

        //[Test]
        //public void TestOrderGetByUnpaidTransaction()
        //{
        //    var entity = new StoreOrderModel();
        //    entity.status = "UNPAID";
        //    var result = _order.GetByUnpaidTransaction(entity);

        //    Assert.AreEqual(result.Status, true);
        //}

        //[Test]
        //public void TestOrderGetByFinishedTransaction()
        //{
        //    var entity = new StoreOrderModel();
        //    entity.status = "FINISH";
        //    var result = _order.GetByFinishedTransaction(entity);

        //    Assert.AreEqual(result.Status, true);
        //}
        //END
    }
}