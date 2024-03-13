namespace ET
{
    public interface IAccountMessage: IMessage
    {
    
    }
    
    public interface IAccountRequest: IAccountMessage, IRequest
    {
    }

    public interface IAccountResponse: IAccountMessage, IResponse
    {
    }
}

