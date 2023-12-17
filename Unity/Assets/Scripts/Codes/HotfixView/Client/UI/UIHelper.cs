namespace ET.Client
{
    public static class UIHelper
    {
        public static void PopMsg(Entity entity, string msg)
        {
            entity.Root().GetComponent<UIComponent>().GetDlgLogic<UIPop>().PopMsg(msg);
        }

        public static async ETTask SetSprite(Entity entity, ExtendImage image, string spriteName, AtlasType atlasType)
        {
            var path = entity.Scene().GetComponent<UIComponent>().GetAtlasPath(atlasType);
            var sp = await entity.LoadSpriteAsync(path, spriteName);
            image.sprite = sp;
        }
    }
}