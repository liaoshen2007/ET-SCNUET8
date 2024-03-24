namespace ET.Client
{
    [NumericWatcher(SceneType.All,NumericType.Exp)]
    public class NumericWatcher_AddExp:INumericWatcher
    {
        public void Run(Unit unit, NumbericChange args)
        {
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            int unitLevel = numericComponent.GetAsInt(NumericType.Level);

            if (PlayerLevelConfigCategory.Instance.Contain(unitLevel))
            {
                long needExp = PlayerLevelConfigCategory.Instance.Get(unitLevel).NeedExp;
                if (args.New>=needExp)
                {
                    Log.Warning("RedDotHelperUpLevelButton");
                    //RedDotHelper.ShowRedDotNode(unit.ZoneScene(), "UpLevelButton");
                }
                else
                {
                    Log.Warning("RedDotHelperUpLevelButton");
                    // if (RedDotHelper.IsLogicAlreadyShow(unit.ZoneScene(),"UpLevelButton"))
                    // {
                    //     RedDotHelper.HideRedDotNode(unit.ZoneScene(), "UpLevelButton");
                    // }
                }

            }
            unit.Root().GetComponent<UIComponent>().GetDlgLogic<UIRoleProperties>()?.Refresh();
        }
    }
    
    [NumericWatcher(SceneType.All,NumericType.AttrPoint)]
    public class NumericWatcher_AttrPoint:INumericWatcher
    {
        public void Run(Unit unit, NumbericChange args)
        {
            if (args.New>0)
            {
                //RedDotHelper.ShowRedDotNode(unit.ZoneScene(), "AddAttribute");
                Log.Warning("AddAttribute");
            }
            else
            {
                // if (RedDotHelper.IsLogicAlreadyShow(unit.ZoneScene(), "AddAttribute"))
                // {
                //     RedDotHelper.HideRedDotNode(unit.ZoneScene(), "AddAttribute");
                // }
                Log.Warning("AddAttribute");
            }
            unit.Root().GetComponent<UIComponent>().GetDlgLogic<UIRoleProperties>()?.Refresh();
        }
    }
}

