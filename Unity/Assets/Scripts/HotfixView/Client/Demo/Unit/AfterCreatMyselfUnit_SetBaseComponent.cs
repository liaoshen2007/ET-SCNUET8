using System;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class AfterCreatMyselfUnit_SetBaseComponent: AEvent<Scene, CreatMySelfUnit>
    {
        protected override async ETTask Run(Scene scene, CreatMySelfUnit args)
        {
            try
            {
                Log.Debug("AfterMyUnitCreate");
                CameraComponent camera = args.CurrentScene.GetComponent<CameraComponent>();
                if (camera==null)
                {
                    args.CurrentScene.AddComponent<CameraComponent>();
                    camera = args.CurrentScene.GetComponent<CameraComponent>();
                }

                var myGo = args.Unit.GetComponent<GameObjectComponent>().GameObject;
                camera.Lock(myGo.transform);
                
                OperaComponent operaComponent=args.CurrentScene.GetComponent<OperaComponent>();
                if (operaComponent==null)
                {
                    args.CurrentScene.AddComponent<OperaComponent>();
                    operaComponent=args.CurrentScene.GetComponent<OperaComponent>();
                }
                Log.Warning("operaComponent.SetUnit(args.Unit)??");
                operaComponent.SetUnit(myGo);


 
                
            }
            catch (Exception e)
            {
                Log.Debug(e.ToString());
            }
        }
    }
}

