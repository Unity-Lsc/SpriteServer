﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpriteServer.Model.Logic.Entitys
{
    /// <summary>
    /// 任务实体
    /// </summary>
    public class TaskEntity
    {
        /// <summary>
        /// 任务编号
        /// </summary>
        public int TaskId;

        /// <summary>
        /// 当前状态
        /// </summary>
        public byte CurStatus;

    }
}
