using System.Net;

namespace ET.Server
{
    [HttpHandler(SceneType.Account, "/account")]
    [FriendOf(typeof (Account))]
    public class HttpAccountHandler: IHttpHandler
    {
        public async ETTask Handle(Scene root, HttpListenerContext context)
        {
            HttpAccount resp = new();
            if (root.GetComponent<SessionLockComponent>() != null)
            {
                resp.Error = ErrorCode.ERR_RequestRepeatedly;
                HttpHelper.Response(context, string.Empty);
                //root.Disconnect().Coroutine();
                return;
            }

            var account = context.Request.QueryString["Account"];
            if (account.IsNullOrEmpty())
            {
                resp.Error = ErrorCode.ERR_InputInvaid;
                HttpHelper.Response(context, string.Empty);
                return;
            }

            var acc = await root.GetComponent<AccountComponent>().GetAccount(account);
            resp.Account = new AccountProto() { Id = acc.Id, AccountType = (int) acc.AccountType, CreateTime = acc.CreateTime, };
            HttpHelper.Response(context, resp);
            await ETTask.CompletedTask;
        }
    }
}