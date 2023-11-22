using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    public enum ItemType
    {
        /// <summary>
        /// 资源道具
        /// </summary>
        Resource = 10,

        /// <summary>
        /// 普通道具
        /// </summary>
        Normal = 11,
    }

    [ChildOf]
    public class ItemData: Entity, IAwake, IDestroy
    {
        public long Count { get; set; }

        public long ValidTime { get; set; }

        [BsonIgnore]
        public ItemType ItemType => (ItemType)this.Config.Type;

        [BsonIgnore]
        public ItemConfig Config
        {
            get
            {
                return ItemConfigCategory.Instance.Get((int)this.Id);
            }
        }
    }
}