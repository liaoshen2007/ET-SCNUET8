using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET.Server
{
    /// <summary>
    /// 需要缓存的实体继承此接口
    /// </summary>
    public interface ICache
    {
        
    }
    
    [ChildOf(typeof(CacheComponent))]
    public class UnitCache : Entity, IAwake, IDestroy
    {
        public string TypeName { get; set; }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<long, Entity> componentDict = new Dictionary<long, Entity>();
        
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<long, long> updateTimeDict = new Dictionary<long, long>();

        /// <summary>
        /// 数据缓存间隔
        /// </summary>
        public const int Interval = 3 * 3600 * 1000;
    }
}