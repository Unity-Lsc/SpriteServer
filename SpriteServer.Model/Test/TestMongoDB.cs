using SpriteServer.Model.Logic.DBModels;
using SpriteServer.Model.Logic.Entitys;

namespace SpriteServer.Model.Test
{
    /// <summary>
    /// 测试
    /// </summary>
    public static class TestMongoDB
    {
        /// <summary>
        /// 添加数据测试
        /// </summary>
        public static void TestAdd() {
            int id = 1;
            for (int i = 0; i < 20; i++) {
                RoleDBModel model = new RoleDBModel();
                var entity = new RoleEntity();
                entity.SFId = id++;
                entity.NickName = "LSC" + new Random().Next(1, 8888);
                entity.Level = 1;
                entity.CreateTime = DateTime.Now;
                entity.UpdateTime = DateTime.Now;
                model.Add(entity);
            }
        }

    }
}
