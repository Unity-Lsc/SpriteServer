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

        #region 查询实体

        /// <summary>
        /// 根据编号获取实体
        /// </summary>
        /// <param name="sfId">编号</param>
        public T GetEntity(long sfId) {
            var collection = GetCollection();
            var filter = Builders<T>.Filter.Eq(t => t.SFId, sfId);
            return collection.Find(filter).FirstOrDefault();
        }

        /// <summary>
        /// 根据条件获取实体
        /// </summary>
        /// <param name="filter">条件过滤</param>
        public T GetEntity(FilterDefinition<T> filter) {
            var collection = GetCollection();
            return collection.Find(filter).FirstOrDefault();
        }

        /// <summary>
        /// 根据编号 异步获取实体
        /// </summary>
        /// <param name="sfId">编号</param>
        public async Task<T> GetEntytyAsync(long sfId) {
            var collection = GetCollection();
            var filter = Builders<T>.Filter.Eq(t => t.SFId, sfId);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        #endregion 查询实体end

        #region 根据条件查询数据集合

        /// <summary>
        /// 根据条件查询数据集合
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <param name="sort">排序</param>
        /// <param name="skip">跳过</param>
        /// <param name="limit">限制数量</param>
        public List<T> GetList(FilterDefinition<T> filter, SortDefinition<T>? sort = null, int skip= 0, int limit = int.MaxValue) {
            var collection = GetCollection();
            return collection.Find(filter).Sort(sort).Limit(limit).ToList();
        }

        /// <summary>
        /// 根据条件查询数据集合
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <param name="count">符合条件的数量</param>
        /// <param name="field">要查询哪些字段</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public List<T> GetList(FilterDefinition<T> filter, out long count, string[]? field = null, SortDefinition<T>? sort = null) {
            var collection = GetCollection();
            count = collection.CountDocuments(filter);

            //不指定查询字段
            if (field == null || field.Length == 0) { 
                if(sort == null) return collection.Find(filter).ToList();
                return collection.Find(filter).Sort(sort).ToList();
            }

            //指定查询字段
            var fieldList = new List<ProjectionDefinition<T>>();
            for (int i = 0; i < field.Length; i++) {
                fieldList.Add(Builders<T>.Projection.Include(field[i].ToString()));
            }
            var proection = Builders<T>.Projection.Combine(fieldList);
            fieldList?.Clear();

            //不排序
            if(sort == null) return collection.Find(filter).Project<T>(proection).ToList();
            //排序
            return collection.Find(filter).Sort(sort).Project<T>(proection).ToList();

        }

        public async Task<List<T>> GetListAsync(FilterDefinition<T> filter, string[]? field = null, SortDefinition<T>? sort = null) {
            var collection = GetCollection();

            //不指定查询字段
            if (field == null || field.Length == 0) {
                if (sort == null) return await collection.Find(filter).ToListAsync();
                return await collection.Find(filter).Sort(sort).ToListAsync();
            }

            //指定查询字段
            var fieldList = new List<ProjectionDefinition<T>>();
            for (int i = 0; i < field.Length; i++) {
                fieldList.Add(Builders<T>.Projection.Include(field[i].ToString()));
            }
            var proection = Builders<T>.Projection.Combine(fieldList);
            fieldList?.Clear();

            //不排序
            if (sort == null) return await collection.Find(filter).Project<T>(proection).ToListAsync();
            //排序
            return await collection.Find(filter).Sort(sort).Project<T>(proection).ToListAsync();
        }

        #endregion 根据条件查询数据集合end

        #region 添加数据

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

        #endregion 添加数据end

        #region 查询数量

        /// <summary>
        /// 获取查询的数量
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <returns>返回符合要求的数量</returns>
        public long GetCount(FilterDefinition<T> filter) {
            var collection = GetCollection();
            return collection.CountDocuments(filter);
        }

        /// <summary>
        /// 异步获取查询的数量
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <returns>返回符合要求的数量</returns>
        public async Task<long> GetCountAsync(FilterDefinition<T> filter) {
            var collection = GetCollection();
            return await collection.CountDocumentsAsync(filter);
        }

        #endregion 查询数量end

        #region 查询分页集合

        /// <summary>
        /// 查询分页集合
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="count">总数量</param>
        /// <param name="field">字段</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public List<T> GetListByPage(FilterDefinition<T> filter, int pageSize, int pageIndex, out long count, string[]? field = null, SortDefinition<T>? sort = null) {
            var collection = GetCollection();
            count = collection.CountDocuments(filter);

            //不指定字段查询
            if (field == null || field.Length == 0) { 
                if(sort == null) return collection.Find(filter).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();
                return collection.Find(filter).Sort(sort).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();
            }

            //指定字段查询
            var fieldList = new List<ProjectionDefinition<T>>();
            for (int i = 0; i < field.Length; i++) {
                fieldList.Add(Builders<T>.Projection.Include(field[i].ToString()));
            }
            var projection = Builders<T>.Projection.Combine(fieldList);
            fieldList?.Clear();

            //不排序
            if (sort == null) return collection.Find(filter).Project<T>(projection).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();
            return collection.Find(filter).Sort(sort).Project<T>(projection).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();
        }

        /// <summary>
        /// 异步查询分页集合
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="field">字段</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public async Task<List<T>> GetListByPageAsync(FilterDefinition<T> filter, int pageSize, int pageIndex, string[]? field = null, SortDefinition<T>? sort = null) {
            var collection = GetCollection();

            //不指定字段查询
            if (field == null || field.Length == 0) {
                if (sort == null) return await collection.Find(filter).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();
                return await collection.Find(filter).Sort(sort).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();
            }

            //指定字段查询
            var fieldList = new List<ProjectionDefinition<T>>();
            for (int i = 0; i < field.Length; i++) {
                fieldList.Add(Builders<T>.Projection.Include(field[i].ToString()));
            }
            var projection = Builders<T>.Projection.Combine(fieldList);
            fieldList?.Clear();

            //不排序
            if (sort == null) return await collection.Find(filter).Project<T>(projection).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();
            return await collection.Find(filter).Sort(sort).Project<T>(projection).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();
        }

        #endregion 查询分页集合end

        #region 修改实体

        /// <summary>
        /// 修改实体
        /// </summary>
        public void Update(T entity) {
            var collection = GetCollection();
            var filter = Builders<T>.Filter.Eq("SFId", entity.SFId);
            collection.FindOneAndReplace(filter, entity);
        }

        /// <summary>
        /// 异步修改实体
        /// </summary>
        public async void UpdateAsync(T entity) {
            var collection = GetCollection();
            var filter = Builders<T>.Filter.Eq("SFId", entity.SFId);
            await collection.FindOneAndReplaceAsync(filter, entity);
        }

        #endregion 修改实体end

    }
}
