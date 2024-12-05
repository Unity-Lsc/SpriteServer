using MongoDB.Driver;
using SpriteServer.Core.SFMongoDB;

namespace SpriteServer.Model.Logic.DBModels
{
    public class UniqueIDGameServer : SFUniqueIDBase
    {
        protected override MongoClient Client => MongoDBClient.CurClient;

        protected override string DatabaseName => "GameServer";

        protected override string CollectionName => "UniqueIDGameServer";
    }
}
