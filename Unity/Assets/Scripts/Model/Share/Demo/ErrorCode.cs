using System;
using System.Collections.Generic;

namespace ET
{
    /// <summary>
    /// 错误码定义(此文件由代码生成, 禁止修改)
    /// </summary>
	public static partial class ErrorCode
	{
		/// <summary>
		/// 网络错误
		/// </summary>
		public const int ERR_NetWorkError = 200001;

		/// <summary>
		/// 请求过快
		/// </summary>
		public const int ERR_RequestRepeatedly = 200002;

		/// <summary>
		/// 找不到配置
		/// </summary>
		public const int ERR_CantFindCfg = 200003;

		/// <summary>
		/// 输入参数有误
		/// </summary>
		public const int ERR_InputInvaid = 200004;

		/// <summary>
		/// 其他人登录
		/// </summary>
		public const int ERR_OtherAccountLogin = 200101;

		/// <summary>
		/// 0
		/// </summary>
		public const int ERR_SessionError = 200102;

		/// <summary>
		/// 没有该玩家
		/// </summary>
		public const int ERR_NoPlayer = 200103;

		/// <summary>
		/// 进入游戏出错
		/// </summary>
		public const int ERR_EnterGame = 200104;

		/// <summary>
		/// 0
		/// </summary>
		public const int ERR_ReEnterGame = 200105;

		/// <summary>
		/// 玩家名字重复
		/// </summary>
		public const int ERR_RoleNameSame = 200106;

		/// <summary>
		/// 角色不存在
		/// </summary>
		public const int ERR_RoleNotExist = 200107;

		/// <summary>
		/// 任务未完成
		/// </summary>
		public const int ERR_TaskNotFinish = 200201;

		/// <summary>
		/// 任务已提交
		/// </summary>
		public const int ERR_TaskIsCommit = 200202;

		/// <summary>
		/// 任务已超时
		/// </summary>
		public const int ERR_TaskIsTimeOut = 200203;

		/// <summary>
		/// 道具不足
		/// </summary>
		public const int ERR_ItemNotEnough = 200301;

		/// <summary>
		/// 找不到讨论组
		/// </summary>
		public const int ERR_ChatCantFindGroup = 200401;

		/// <summary>
		/// 讨论组已存在
		/// </summary>
		public const int ERR_ChatGroupExist = 200402;

		/// <summary>
		/// 执行GS命令出错
		/// </summary>
		public const int ERR_GSCmdError = 200501;

		/// <summary>
		/// 没有找到GS命令
		/// </summary>
		public const int ERR_GSCmdNotFound = 200502;

		/// <summary>
		/// 对象已死亡
		/// </summary>
		public const int ERR_UnitDead = 200601;

		/// <summary>
		/// 找不到目标
		/// </summary>
		public const int ERR_UnitNotExit = 200602;
	}
}
