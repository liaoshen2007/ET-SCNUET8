using MongoDB.Driver;

namespace ET.Server
{
    [EntitySystemOf(typeof (AccountComponent))]
    [FriendOf(typeof (Account))]
    public static partial class AccountComponentSystem
    {
        [EntitySystem]
        private static void Awake(this AccountComponent self)
        {
            var collection = self.Scene().GetComponent<DBManagerComponent>().GetDB().GetCollection<Account>();
            collection.Indexes.CreateMany(new[]
            {
                new CreateIndexModel<Account>(Builders<Account>.IndexKeys.Ascending(info => info.Id)),
                new CreateIndexModel<Account>(Builders<Account>.IndexKeys.Ascending(info => info.AccountName)),
            });
        }

        /// <summary>
        /// 获取账号信息
        /// </summary>
        /// <returns></returns>
        public static async ETTask<Account> GetAccount(this AccountComponent self, string account, string password = "")
        {
            var list = await self.Scene().GetComponent<DBManagerComponent>().GetDB().Query<Account>(info => info.AccountName == account);
            if (list.Count > 0)
            {
                return list[0];
            }

            var acc = self.AddChild<Account>();
            acc.AccountName = account;
            acc.Password = password;
            acc.CreateTime = TimeInfo.Instance.ServerFrameTime();
            acc.AccountType = AccountType.General;
            await self.Scene().GetComponent<DBManagerComponent>().GetDB().Save(self.Id, acc);

            acc.Dispose();
            return acc;
        }
    }
}