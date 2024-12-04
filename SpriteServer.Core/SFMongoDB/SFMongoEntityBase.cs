using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace SpriteServer.Core.SFMongoDB
{
    /// <summary>
    /// MongoEntity基类
    /// </summary>
    [BsonIgnoreExtraElements]
    public class SFMongoEntityBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public ObjectId Id;

        /// <summary>
        /// 主键
        /// </summary>
        public long SFId;

        /// <summary>
        /// 状态
        /// </summary>
        public DataStatus Status;

        /// <summary>
        /// 创建的时间
        /// </summary>
        public DateTime CreateTime;

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime;

    }
}
