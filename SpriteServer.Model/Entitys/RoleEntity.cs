using SpriteServer.Core.SFMongoDB;

namespace SpriteServer.Model.Entitys
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
    }
}
