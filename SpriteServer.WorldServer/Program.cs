using SpriteServer.Model.DBModels;
using SpriteServer.Model.Entitys;

namespace SpriteServer.WorldServer
{
    //中心服务器 单节点控制台
    internal class Program
    {
        static void Main(string[] args) {

            Console.WriteLine("中心服务器 启动...");

            RoleDBModel model = new RoleDBModel();
            var entity = new RoleEntity();
            entity.SFId = 1;
            entity.NickName = "LSC";
            entity.Level = 1;
            entity.CreateTime = DateTime.Now;
            entity.UpdateTime = DateTime.Now;
            model.Add(entity);

            Console.ReadLine();
        }
    }
}
