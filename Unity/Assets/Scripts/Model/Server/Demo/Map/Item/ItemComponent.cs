using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET.Server
{
    public enum ItemMode
    {
        /// <summary>
        /// 开宝箱
        /// </summary>
        OpenBox = 1,
    }

    public struct AddItemData
    {
        public int LogEvent { get; set; }

        public bool NotUpdate { get; set; }
    }

    [ComponentOf(typeof (Unit))]
    public class ItemComponent: Entity, IAwake, IDestroy, ILoad, ICache, ITransfer
    {
        public HashSet<int> InitItemIds = new HashSet<int>();
    
        /// <summary>
        /// 所有道具字典
        /// </summary>
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, ItemData> ItemDict = new Dictionary<int, ItemData>();

        /// <summary>
        /// 期限道具
        /// </summary>
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, ItemData> ValidItemDict = new Dictionary<int, ItemData>();
    }
}