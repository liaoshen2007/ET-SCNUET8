namespace ET;

public static class ItemDataSystem
{
    public static ItemProto ToItemProto(this ItemData self)
    {
        return new ItemProto() { Id = (int)self.Id, Count = self.Count, ValidTime = self.ValidTime };
    }

    public static void ToItem(this ItemData self, ItemProto item)
    {
        self.Count = item.Count;
        self.ValidTime = item.ValidTime;
    }
}