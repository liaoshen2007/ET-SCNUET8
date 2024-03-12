namespace ET.Client
{
    [ComponentOf(typeof (Scene))]
    public class AccountInfoComponent:Entity,IAwake,IDestroy
    {
        public string Token { get; set; }//一定要get;set;有学问！
        public long AccountId { get; set; }

        public string RealmKey{ get; set; }

        public string ReamlAddress{ get; set; }

    }
}

