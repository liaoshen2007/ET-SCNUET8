namespace ET
{
    public static partial class ErrorCode
    {
        public const int ERR_Success = 0;

        // 1-11004 是SocketError请看SocketError定义
        //-----------------------------------
        // 100000-109999是Core层的错误

        // 110000以下的错误请看ErrorCore.cs

        // 这里配置逻辑层的错误码
        // 110000 - 200000是抛异常的错误
        // 200001以上不抛异常

        public const int ERR_NetWorkError = 200008;
        /// <summary>
        /// 其他人登录
        /// </summary>
        public const int ERR_OtherAccountLogin = 200009;

        public const int ERR_SessionError = 200010;
        public const int ERR_NoPlayer = 200011;
        public const int ERR_EnterGame = 200012;
        public const int ERR_ReEnterGame = 200013;
        public const int ERR_RoleNameSame = 200014;
        public const int ERR_RoleNotExist = 200015;

        /// <summary>
        /// 请求过快
        /// </summary>
        public const int ERR_RequestRepeatedly = 210001;

        /// <summary>
        /// 找不到配置
        /// </summary>
        public const int ERR_CantFindCfg = 210002;

        /// <summary>
        /// 输入参数有误
        /// </summary>
        public const int ERR_InputInvaid = 210003;
        
        public const int ERR_GSCmdError = 210004;
        public const int ERR_GSCmdNotFound = 210004;
        
        /// <summary>
        /// 任务未完成
        /// </summary>
        public const int ERR_TaskNotFinish = 211001;
        public const int ERR_TaskIsCommit = 211002;
        public const int ERR_TaskIsTimeOut = 211003;
    }
}