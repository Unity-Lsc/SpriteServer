using MongoDB.Driver;
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
            RoleDBModel model = new RoleDBModel();
            UniqueIDGameServer server = new UniqueIDGameServer();
            int level = 1;
            for (int i = 0; i < 8; i++) {
                var entity = new RoleEntity();
                entity.SFId = server.GetUniqueID((int)CollectionType.Role);
                entity.NickName = "LSC" + new Random().Next(1, 8888);
                entity.Level = level++;
                entity.CreateTime = DateTime.Now;
                entity.UpdateTime = DateTime.Now;

                entity.TaskList.Add(new TaskEntity() { TaskId = 1, CurStatus = 0 });
                entity.TaskList.Add(new TaskEntity() { TaskId = 2, CurStatus = 1 });

                entity.SkillDict[0] = new SkillEntity() { SkillId = 1, CurLevel = 2 };
                entity.SkillDict[1] = new SkillEntity() { SkillId = 2, CurLevel = 3 };
                entity.SkillDict[2] = new SkillEntity() { SkillId = 3, CurLevel = 4 };

                model.AddAsync(entity);
            }
            Console.WriteLine("添加完毕...");
        }

        public static void TestSearch() {
            RoleDBModel model = new RoleDBModel();
            var filter = Builders<RoleEntity>.Filter.Eq(t => t.SFId, 2);
            RoleEntity entity = model.GetEntity(filter);
            if(entity != null) {
                Console.WriteLine("roleEntity:" + entity.NickName);
                if (entity.SkillDict.TryGetValue(1, out var skillEntity)) {
                    Console.WriteLine("skillEntity:" + skillEntity.CurLevel);
                }
                else {
                    Console.WriteLine("技能不存在");
                    entity.SkillDict[1] = new SkillEntity() { SkillId = 1, CurLevel = 2 };
                    model.Update(entity);
                }
            }
            else {
                Console.WriteLine("roleEntity不存在");
            }
        }

        public static void TestUniqueID() {
            UniqueIDGameServer server = new UniqueIDGameServer();
            long roleId = server.GetUniqueID(0);
            Console.WriteLine("角色编号:" + roleId);
        }

    }
}
