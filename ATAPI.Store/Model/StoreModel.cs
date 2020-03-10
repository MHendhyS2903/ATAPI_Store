using ATAPI.Core.Model;
using ATAPI.Store.Interface;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATAPI.Store.Model
{
    public class StoreModel
    {
        public string updatedby { get; set; }
        public string createdby { get; set; }
        public string status { get; set; }
        public string token { get; set; }
        public string reference { get; set; }
        public string bankbook { get; set; }
        public string title { get; set; }
        public string banknumber { get; set; }
        public string bank { get; set; }
        public string npwp { get; set; }
        public string npwpnumber { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? created_at { get; set; }
        public string address { get; set; }
        public string ktp { get; set; }
        public string nik { get; set; }
        public string identitytype { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string hp2 { get; set; }
        public string hp1 { get; set; }
        public string owner { get; set; }
        public string storename { get; set; }
        public ObjectId id { get; set; }
        public int domicilecity { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? updated_at { get; set; }

        //public Task<ResponseModel<StoreModel>> Delete(string token, ObjectId id)
        //{
        //    throw new NotImplementedException();
        //}

        //public ResponseModel<StoreModel> DeleteTest(string token, ObjectId id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ResponseModel<StoreModel>> GetByID(string token, ObjectId id)
        //{
        //    throw new NotImplementedException();
        //}

        //public ResponseModel<StoreModel> GetByIDTest(string token, ObjectId id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ResponseModel<IEnumerable<StoreModel>>> GetByParams(string auth, string display, string filterparams)
        //{
        //    throw new NotImplementedException();
        //}

        //public ResponseModel<IEnumerable<StoreModel>> GetByParamsTest(string auth, string display, string filterparams)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ResponseModel<StoreModel>> GetByToken(string token, string fbtoken)
        //{
        //    throw new NotImplementedException();
        //}

        //public ResponseModel<StoreModel> GetByTokenTest(string token, string fbtoken)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ResponseModel<long>> IsExists(StoreModel entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public ResponseModel<long> IsExistsTest(StoreModel entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ResponseModel<StoreModel>> Login(string cellular, string password)
        //{
        //    throw new NotImplementedException();
        //}

        //public ResponseModel<StoreModel> LoginTest(string cellular, string password)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ResponseModel<int>> Registration(StoreModel entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public ResponseModel<int> RegistrationTest(StoreModel entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ResponseModel<StoreModel>> Update(StoreModel entity, string token)
        //{
        //    throw new NotImplementedException();
        //}

        //public ResponseModel<StoreModel> UpdateTest(StoreModel entity, string token)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
