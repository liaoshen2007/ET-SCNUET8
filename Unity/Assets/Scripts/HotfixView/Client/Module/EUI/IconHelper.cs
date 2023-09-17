using System;
using UnityEngine;
using UnityEngine.U2D;

namespace ET.Client
{
    public static class IconHelper
    {
        /// <summary>
        /// 异步加载图集图片资源
        /// </summary>
        /// <OtherParam name="spriteName"></OtherParam>
        /// <returns></returns>
        public static async ETTask<Sprite> LoadIconSpriteAsync(this Entity self, string atlasName, string spriteName)
        {
            try
            {
                SpriteAtlas spriteAtlas =
                        await self.Scene().GetComponent<ResourcesLoaderComponent>().LoadAssetAsync<SpriteAtlas>(atlasName);
                Sprite sprite = spriteAtlas.GetSprite(spriteName);
                if (null == sprite)
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