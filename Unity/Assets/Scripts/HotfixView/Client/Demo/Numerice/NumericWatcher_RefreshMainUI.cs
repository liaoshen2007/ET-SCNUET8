namespace ET.Client
{
    [NumericWatcher(SceneType.All,NumericType.Level)]
    public class NumericWatcher_LevelRefreshMainUI:INumericWatcher
    {
        public void Run(Unit unit, NumbericChange args)
        {
            unit.Root().GetComponent<UIComponent>().GetDlgLogic<UIRoleProperties>()?.Refresh();
            //Log.Error("NumericType.Level"+args);
        }
    }
    
    [NumericWatcher(SceneType.All,NumericType.Coin)]
    public class NumericWatcher_CoinRefreshMainUI:INumericWatcher
    {
        public void Run(Unit unit, NumbericChange args)
        {
            unit.Root().GetComponent<UIComponent>().GetDlgLogic<UIRoleProperties>()?.Refresh();
            //Log.Error("NumericType.Coin"+args);
        }
    }
    
    [NumericWatcher(SceneType.All,NumericType.Exp)]
    public class NumericWatcher_ExpRefreshMainUI:INumericWatcher
    {
        public void Run(Unit unit, NumbericChange args)
        {
            unit.Root().GetComponent<UIComponent>().GetDlgLogic<UIRoleProperties>()?.Refresh();
            //Log.Error("NumericType.Exp"+args);
        }
    }
}

