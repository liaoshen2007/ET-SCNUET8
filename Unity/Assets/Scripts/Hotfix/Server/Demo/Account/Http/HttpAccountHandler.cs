using System.Net;

namespace ET.Server;

[HttpHandler(SceneType.Account, "/account")]
[FriendOf(typeof (Account))]
public class HttpAccountHandler: IHttpHandler
{
    public async ETTask Handle(Scene scene, HttpListenerContext context)
    {
        HttpAccount resp = new();
        var account = context.Request.QueryString["Account"];
        if (account.IsNullOrEmpty())
        {
            resp.Error = ErrorCode.ERR_InputInvaid;
            HttpHelper.Response(context, string.Empty);
            return;
        }

        var acc = await scene.GetComponent<AccountComponent>().GetAccount(account);
        resp.Account = new AccountProto() { Id = acc.Id, AccountType = (int) acc.AccountType, CreateTime = acc.CreateTime, UserUid = acc.UserUid };
        HttpHelper.Response(context, resp);
        await ETTask.CompletedTask;
    }
}