namespace ET
{
    public interface IRankMessage: IMessage
    {
    }

    public interface IRankRequest: IRankMessage, IRequest
    {
    }

    public interface IRankResponse: IRankMessage, IResponse
    {
    }
}