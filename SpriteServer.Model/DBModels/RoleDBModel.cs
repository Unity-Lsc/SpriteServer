using MongoDB.Driver;
using SpriteServer.Core.SFMongoDB;
using SpriteServer.Model.Entitys;

namespace SpriteServer.Model.DBModels
{
    /// <summary>
    /// 角色的数据管理
    /// </summary>
    public class RoleDBModel : SFMongoDBModelBase<RoleEntity>
    {
        protected override MongoClient Client => MongoDBClient.CurClient;

        protected override string DatabaseName => "GameServer";

        protected override string CollectionName => "Role";
    }
}
