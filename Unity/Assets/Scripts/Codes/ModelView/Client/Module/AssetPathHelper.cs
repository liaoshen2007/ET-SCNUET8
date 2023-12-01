namespace ET.Client
{
    /// <summary>
    /// AB实用函数集，主要是路径拼接
    /// </summary>
    public static class AssetPathHelper
    {
        public static string GetTexturePath(string fileName)
        {
            return $"Assets/Bundles/Altas/{fileName}.prefab";
        }
        
        /// <summary>
        /// 获取ui路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ToUIPath(this string fileName)
        {
            var winCfg = WindowCategory.Instance.GetWindow(fileName);
            return $"Assets/Bundles/UI/Window/{winCfg.Path}.prefab";
        }
        
        public static string ToUISpriteAtlasPath(this string fileName)
        {
            return $"Assets/Bundles/UI/Atlas/{fileName}.spriteatlas";
        }
        
        public static string GetNormalConfigPath(string fileName)
        {
            return $"Assets/Bundles/Independent/{fileName}.prefab";
        }
 
        public static string ToSoundPath(this string fileName)
        {
            return $"Assets/Bundles/Sound/{fileName}.mp3";
        }
        
        public static string ToConfigPath(this string fileName)
        {
            return $"Assets/Bundles/Config/{fileName}.bytes";
        }
        
        public static string ToSQLiteDBPath(this string fileName)
        {
            return $"Assets/Bundles/SQLiteDB/{fileName}.db";
        }
 
        public static string GetSkillConfigPath(string fileName)
        {
            return $"Assets/Bundles/SkillConfigs/{fileName}.prefab";
        }
 
        public static string ToPrefabPath(this string fileName)
        {
            return $"Assets/Bundles/Prefab/{fileName}.prefab";
        }
 
        public static string GetScenePath(this string fileName)
        {
            //return $"Assets/Scenes/{fileName}.unity";
            return $"{fileName}.unity";
        }

        public static string ToUICommonPath(this string fileName)
        {
            return $"Assets/Bundles/UI/Common/{fileName}.prefab";
        }
        
        public static string ToUIItemPath(this string fileName)
        {
            return $"Assets/Bundles/UI/Item/{fileName}.prefab";
        }
        
        public static string ToUnitModelPath(this string fileName)
        {
            return $"Assets/Bundles/Unit/{fileName}.prefab";
        }

        public static string ToUnitHUDPath(this string fileName)
        {
            return $"Assets/Bundles/UnitHUD/{fileName}HUD.prefab";
        }
        
    }
}