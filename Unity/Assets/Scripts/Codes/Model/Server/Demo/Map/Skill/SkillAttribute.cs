namespace ET
{
    public class SkillAttribute : BaseAttribute
    {
        public string Cmd { get; }

        public SkillAttribute(string cmd)
        {
            Cmd = cmd;
        }
    }

    public abstract class ASkillEffect
    {
        
    }
}