﻿using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Current)]
    
    public class AfterUnitCreate_CreateUnitView: AEvent<Scene, AfterUnitCreate>
    {
        protected override async ETTask Run(Scene scene, AfterUnitCreate args)
        {
            Unit unit = args.Unit;
            // Unit View层
            string assetsName = $"Assets/Bundles/Unit/Unit.prefab";
            GameObject unitGo = await scene.GetComponent<ResourcesLoaderComponent>().LoadAssetAsync<GameObject>(assetsName);
            var prefab = unitGo.GetComponent<ReferenceCollector>().Get<GameObject>(unit.Config().Prefab);

            GameObject go = UnityEngine.Object.Instantiate(prefab, Global.Instance.Unit, true);
            go.transform.position = unit.Position;
            var com = unit.AddComponent<GameObjectComponent>();
            com.SetGo(go);
            //scene.GetComponent<CameraComponent>().Lock(com.ChestTrans);
            
            unit.AddComponent<AnimatorComponent>();
            if (args.IsSelf)
            {
                EventSystem.Instance.Publish(unit.Scene(), new CreatMySelfUnit() {Unit = unit,CurrentScene = scene});
            }
            await ETTask.CompletedTask;
        }
    }
}