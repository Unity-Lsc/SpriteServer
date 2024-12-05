using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using SpriteServer.Core.SFMongoDB;

namespace SpriteServer.Model.Logic.Entitys
{
    /// <summary>
    /// 角色实体
    /// </summary>
    public class RoleEntity : SFMongoEntityBase
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string? NickName;

        /// <summary>
        /// 等级
        /// </summary>
        public int Level;

        /// <summary>
        /// 任务列表
        /// </summary>
        public List<TaskEntity> TaskList;

        /// <summary>
        /// 技能集合
        /// </summary>
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, SkillEntity> SkillDict;

        public RoleEntity() { 
            TaskList = new List<TaskEntity>();
            SkillDict = new Dictionary<int, SkillEntity>();
        }

    }
}
