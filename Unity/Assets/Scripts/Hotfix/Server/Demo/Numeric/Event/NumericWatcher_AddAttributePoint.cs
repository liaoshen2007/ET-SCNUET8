namespace ET.Server
{
    [NumericWatcher(SceneType.Map, NumericType.Strength)]
    public class NumericWatcher_AddAttributeStrengthPoint:INumericWatcher
    {
        public void Run(Unit unit, NumbericChange args)
        {
            unit.GetComponent<NumericComponent>()[NumericType.Attack] += 5;
        }
    }
    
    [NumericWatcher(SceneType.Map, NumericType.Mind)]
    public class NumericWatcher_AddAttributeMindPoint:INumericWatcher
    {
        public void Run(Unit unit, NumbericChange args)
        {
            unit.GetComponent<NumericComponent>()[NumericType.Mana] +=  1 * 10000;
        }
    }
    
    [NumericWatcher(SceneType.Map, NumericType.Agility)]
    public class NumericWatcher_AddAttributeAgilityPoint:INumericWatcher
    {
        public void Run(Unit unit, NumbericChange args)
        {
            unit.GetComponent<NumericComponent>()[NumericType.Defense] += 5;
        }
    }
    
    [NumericWatcher(SceneType.Map, NumericType.Energy)]
    public class NumericWatcher_AddAttributeEnergyPoint:INumericWatcher
    {
        public void Run(Unit unit, NumbericChange args)
        {
            unit.GetComponent<NumericComponent>()[NumericType.MaxHp] += 1 * 10000;
        }
    }
    
    
}

