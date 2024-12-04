using MongoDB.Driver;

namespace SpriteServer.Core.SFMongoDB
{
    /// <summary>
    /// MongoDBModel基类
    /// </summary>
    public abstract class SFMongoDBModelBase<T> where T : SFMongoEntityBase, new()
    {
        /// <summary>
        /// MongoClient
        /// </summary>
        protected abstract MongoClient Client { get; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        protected abstract string DatabaseName { get; }

        /// <summary>
        /// 集合名称
        /// </summary>
        protected abstract string CollectionName { get; }

        /// <summary>
        /// 文档集合
        /// </summary>
        private IMongoCollection<T>? m_Collection = null;

        /// <summary>
        /// 获取文档集合
        /// </summary>
        public IMongoCollection<T> GetCollection() {
            if (m_Collection == null) { 
                IMongoDatabase database = Client.GetDatabase(DatabaseName);
                m_Collection = database.GetCollection<T>(CollectionName);
            }
            return m_Collection;
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">要添加的数据实体</param>
        public void Add(T entity) { 
            var collection = GetCollection();
            collection.InsertOne(entity);
        }

        /// <summary>
        /// 异步添加数据
        /// </summary>
        /// <param name="entity">要添加的数据实体</param>
        public async void AddAsync(T entity) {
            var collection = GetCollection();
            await collection.InsertOneAsync(entity);
        }

        public void AddMany() {

        }

    }
}
