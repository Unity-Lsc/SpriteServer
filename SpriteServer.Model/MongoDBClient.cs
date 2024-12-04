using MongoDB.Driver;

namespace SpriteServer.Model
{
    public static class MongoDBClient
    {

        private static object m_LockObj = new object();

        private static MongoClient m_CurClient = null;
        /// <summary>
        /// 当前的MongoClient
        /// </summary>
        public static MongoClient CurClient {
            get {
                if (m_CurClient == null) {
                    lock (m_LockObj) {
                        m_CurClient ??= new MongoClient("mongodb://127.0.0.1");
                    }
                }
                return m_CurClient;
            }
        }
    }
}
