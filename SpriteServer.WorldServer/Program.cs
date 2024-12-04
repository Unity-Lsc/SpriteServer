using SpriteServer.Model.Test;

namespace SpriteServer.WorldServer
{
    //中心服务器 单节点控制台
    internal class Program
    {
        static void Main(string[] args) {

            Console.WriteLine("中心服务器 启动...");

            TestMongoDB.TestAdd();

            Console.ReadLine();
        }
    }
}
