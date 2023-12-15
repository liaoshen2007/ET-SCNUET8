using System;
using System.Text.RegularExpressions;

namespace ET.Client;

public static class ChatHelper
{
    public static string Encode(ChatMsgData msgData)
    {
        using ListComponent<string> list = ListComponent<string>.Create();
        list.Add(msgData.Msg);

        //表情表
        if (msgData.Emjo != 0)
        {
            list.Add($"<{ChatMsgKeyWord.Emjo}>{msgData.Emjo}</{ChatMsgKeyWord.Emjo}>");
        }

        //回复
        if (msgData.Quote.Id != 0)
        {
            string str = $"{msgData.Quote.Id}{UIChat.Sep}{msgData.Quote.Name}{UIChat.Sep}{msgData.Quote.Msg}";
            string quoteStr = $"<{ChatMsgKeyWord.Quote}>{str}</{ChatMsgKeyWord.Quote}>";
            list.Add(quoteStr);
        }

        //at
        foreach (var data in msgData.AtList)
        {
            string str = $"<{ChatMsgKeyWord.At}>{data.Id}{UIChat.Sep}{data.Name}</{ChatMsgKeyWord.At}>";
            list.Add(str);
        }

        //道具
        foreach (var data in msgData.ItemList)
        {
        }

        if (list.Count > 1)
        {
            list.Insert(1, UIChat.SpecSep);
        }

        return string.Concat(list);
    }

    public static ChatMsgData Decode(string msgData)
    {
        var data = new ChatMsgData();

        int sepIndex = msgData.IndexOf(UIChat.SpecSep, StringComparison.Ordinal);
        while (sepIndex != -1)
        {
            int r = msgData.IndexOf(UIChat.SpecSep, sepIndex + 1, StringComparison.Ordinal);
            if (r == -1) break;

            sepIndex = r;
        }

        if (sepIndex == -1)
        {
            data.Msg = msgData;
            if (msgData.IndexOf("Msg", StringComparison.Ordinal) != -1
                && msgData.IndexOf("MsgArgs", StringComparison.Ordinal) != -1)
            {
            }

            return data;
        }

        string msg = msgData.Substring(0, sepIndex - 1);
        string custom = msgData.Substring(sepIndex);
        data.Msg = msg;

        const string regex = @"<(\w+)>(.?)</(\w+)>";
        int index = 1;
        while (index < custom.Length)
        {
            var match = Regex.Match(custom, regex, RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                break;
            }

            var keyWord = Enum.Parse<ChatMsgKeyWord>(match.Groups[0].Value);
            if (keyWord == ChatMsgKeyWord.Emjo)
            {
                data.Emjo = match.Groups[1].Value.ToInt();
                break;
            }

            if (keyWord == ChatMsgKeyWord.Quote)
            {
                var ll = match.Groups[1].Value.Split(UIChat.Sep);
                data.Quote.Id = ll[0].ToInt();
                data.Quote.Name = ll[1];
                data.Quote.Msg = ll[2];
            }
            else if (keyWord == ChatMsgKeyWord.At)
            {
                var ll = match.Groups[1].Value.Split(UIChat.Sep);
                data.AtList.Add(new AtData() { Id = ll[0].ToInt(), Name = ll[1], });
            }
            else if (keyWord == ChatMsgKeyWord.Item)
            {
            }

            index = match.Index;
        }

        return data;
    }
}