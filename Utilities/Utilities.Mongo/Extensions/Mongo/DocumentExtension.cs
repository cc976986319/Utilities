using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Mongo.Extensions.Mongo
{
    /// <summary>
    /// 文档扩展
    /// </summary>
    public static class DocumentExtension
    {
        /// <summary>
        /// 时间本地化
        /// </summary>
        /// <param name="document">文档</param>
        /// <returns></returns>
        public static BsonDocument ToLocalDateTime(this BsonDocument document)
        {
            // 求时间差
            DateTime currentTime = DateTime.Now;
            TimeSpan ts = currentTime - currentTime.ToUniversalTime();

            var timeItems = (from a in document.Elements where a.Value.BsonType == BsonType.DateTime select a).ToList();
            foreach (BsonElement time in timeItems)
                document[time.Name] = ((DateTime)document[time.Name]).AddHours(ts.Hours).AddMinutes(ts.Minutes).AddSeconds(ts.Seconds).AddMilliseconds(ts.Milliseconds);

            // 改变子文档内的时间字段(递归算法)
            var documentItems = (from a in document.Elements where a.Value.BsonType == BsonType.Document select a).ToList();
            foreach (BsonElement _document in documentItems)
                ((BsonDocument)_document.Value).ToLocalDateTime();

            return document;
        }
    }
}
