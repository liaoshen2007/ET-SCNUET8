namespace ET.Client;

public class ActionAttribute: BaseAttribute
{
    public string ActionName { get; set; }

    public ActionAttribute(string name)
    {
        this.ActionName = name;
    }
}

public abstract class AAction
{
    public virtual void Execute(Unit unit, ActionUnit actionUnit)
    {
    }

    public virtual void OnExecute(Unit unit, ActionUnit actionUnit)
    {
    }

    public virtual void OnUpdate(Unit unit, ActionUnit actionUnit)
    {
    }

    public virtual void OnUnExecute(Unit unit, ActionUnit actionUnit)
    {
    }

    public virtual void OnFinish(Unit unit, ActionUnit actionUnit)
    {
    }
}