using System;
using System.Collections.Generic;

namespace ET
{
    public struct MessageReturn
    {
        public int Errno { get; set; }

        public List<string> Message { get; set; }

        public static MessageReturn Create(int errco, List<string> message = default) => new MessageReturn() { Errno = errco, Message = message, };

        public static MessageReturn Success() => new MessageReturn() { Errno = ErrorCode.ERR_Success, Message = default, };
    }

    public interface IMessageSessionHandler
    {
        void Handle(Session session, object message);

        Type GetMessageType();

        Type GetResponseType();
    }
}