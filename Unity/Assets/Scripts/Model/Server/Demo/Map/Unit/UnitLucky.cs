using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET.Server
{
    /// <summary>
    /// 玩家额外数据
    /// </summary>
    [ComponentOf(typeof (Unit))]
    public class UnitLucky: Entity, IAwake, ITransfer, ICache
    {
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, UnitItemBan> banDict = new Dictionary<int, UnitItemBan>();

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, UnitItemRecover> recoverDict = new Dictionary<int, UnitItemRecover>();

        /// <summary>
        /// 战斗力列表
        /// </summary>
        public List<long> fightList = new List<long>((int) FightType.Max);

        public List<long> flexData = new List<long>();
    }
}