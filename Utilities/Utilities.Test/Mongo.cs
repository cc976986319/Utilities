using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities.Mongo.Models;
using Utilities.Mongo;
using MongoDB.Bson.Serialization.Attributes;
using System.Linq;

namespace Utilities.Test
{
    [TestClass]
    public class Mongo
    {
        [TestMethod]
        public void TestMethod1()
        {
            MongoContext context = new MongoContext("MessageSendingSystem");
            //UserInfo user = new UserInfo();
            //user._id = Guid.NewGuid().ToString();
            //user.Name = "任茂波";
            //user.Account = "SC14002700";
            //user.Data = new UserInfo.Other() {
            //    Id = 12,
            //    Age = 23
            //};
            //context.Add<UserInfo>(user);
            var data = context.AsQueryable<UserInfo>().FirstOrDefault(e => e.Name == "任茂波");
        }
    }
    
    public class UserInfo : MongoEntity
    {
        public string Name { get; set; }

        public string Account { get; set; }

        public Other Data { get; set; }

        public class Other
        {
            [BsonElement("_id")]
            public int Id { get; set; }

            public int Age { get; set; }
        }
    }
}
