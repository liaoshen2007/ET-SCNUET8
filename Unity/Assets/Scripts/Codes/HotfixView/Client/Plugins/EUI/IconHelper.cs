using System;
using UnityEngine;
using UnityEngine.U2D;

namespace ET.Client
{
    public static class IconHelper
    {
        public static async ETTask SetSprite(Entity entity, ExtendImage image, string spriteName, AtlasType atlasType)
        {
            var path = entity.Scene().GetComponent<UIComponent>().GetAtlasPath(atlasType);
            var sp = await entity.LoadSpriteAsync(path, spriteName);
            image.sprite = sp;
        }
        
        public static async ETTask<Sprite> LoadIconSpriteAsync(this Entity self, string spriteName)
        {
            var path = self.Scene().GetComponent<UIComponent>().GetAtlasPath(AtlasType.Icon);
            return await self.LoadSpriteAsync(path, spriteName);
        }

        public static async ETTask<Sprite> LoadWidgetSpriteAsync(this Entity self, string spriteName)
        {
            var path = self.Scene().GetComponent<UIComponent>().GetAtlasPath(AtlasType.Widget);
            return await self.LoadSpriteAsync(path, spriteName);
        }

        /// <summary>
        /// 异步加载图集图片资源
        /// </summary>
        /// <OtherParam name="spriteName"></OtherParam>
        /// <returns></returns>
        public static async ETTask<Sprite> LoadSpriteAsync(this Entity self, string path, string spriteName)
        {
            try
            {
                SpriteAtlas spriteAtlas =
                        await self.Scene().GetComponent<ResourcesLoaderComponent>().LoadAssetAsync<SpriteAtlas>(path);
                Sprite sprite = spriteAtlas.GetSprite(spriteName);
                if (sprite == null)
                {
                    Log.Error($"sprite is null: {spriteName}");
                }

                return sprite;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }
        }
    }
}