using System;
using System.Collections.Generic;

namespace ET
{
    public struct MessageReturn
    {
        public int Errno { get; private set; }

        public List<string> Message { get; private set;}

        public static MessageReturn Create(int errco, List<string> message = default) => new() { Errno = errco, Message = message, };

        public static MessageReturn Success() => new() { Errno = ErrorCode.ERR_Success, Message = new List<string>(), };
    }

    public interface IMessageSessionHandler
    {
        void Handle(Session session, object message);

        Type GetMessageType();

        Type GetResponseType();
    }
}