namespace ET.Client
{
    /// <summary>
    /// 开始切换场景
    /// </summary>
    public struct SceneChangeStart
    {
    }
    
    /// <summary>
    /// 场景切换结束
    /// </summary>
    public struct SceneChangeFinish
    {
    }
    
    /// <summary>
    /// 场景创建成功事件
    /// </summary>
    public struct AfterCreateCurrentScene
    {
    }

    /// <summary>
    /// 游戏启动成功事件
    /// </summary>
    public struct AppStartInitFinish
    {
    }

    /// <summary>
    /// 登录完成
    /// </summary>
    public struct LoginFinish
    {
    }

    public struct EnterMapFinish
    {
    }

    /// <summary>
    /// 角色创建完成
    /// </summary>
    public struct AfterUnitCreate
    {
        public Unit Unit;
    }
}