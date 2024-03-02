using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class SceneChangeStart_AddComponent: AEvent<Scene, SceneChangeStart>
    {
        protected override async ETTask Run(Scene root, SceneChangeStart args)
        {
            try
            {
                Scene currentScene = root.CurrentScene();

                ResourcesLoaderComponent resourcesLoaderComponent = currentScene.GetComponent<ResourcesLoaderComponent>();

                // 加载场景资源
                string path = $"Assets/Bundles/Scenes/{currentScene.Name}.unity";
                var handler = resourcesLoaderComponent.LoadScene(path, LoadSceneMode.Single);
                while (true)
                {
                    EventSystem.Instance.Publish(root, new LoadingProgress() { Progress = handler.Progress });
                    await root.GetComponent<TimerComponent>().WaitFrameAsync();
                    if (handler.IsDone)
                    {
                        break;
                    }
                }

                currentScene.AddComponent<OperaComponent>();
                currentScene.AddComponent<CameraComponent>();
                switch (Application.platform)
                {
                    case RuntimePlatform.OSXEditor:
                    case RuntimePlatform.LinuxEditor:
                    case RuntimePlatform.WindowsEditor:
                        currentScene.AddComponent<PcInputComponent>();
                        break;
                    case RuntimePlatform.OSXPlayer:
                    case RuntimePlatform.WindowsPlayer:
                    case RuntimePlatform.LinuxPlayer:
                        currentScene.AddComponent<PcInputComponent>();
                        break;
                    case RuntimePlatform.IPhonePlayer:
                    case RuntimePlatform.Android:
                        currentScene.AddComponent<PhoneComponent>();
                        break;
                    case RuntimePlatform.WebGLPlayer:
                        break;
                    case RuntimePlatform.PS4:
                        break;
                    case RuntimePlatform.XboxOne:
                        break;
                    case RuntimePlatform.tvOS:
                        break;
                    case RuntimePlatform.Switch:
                        break;
                    case RuntimePlatform.Lumin:
                        break;
                    case RuntimePlatform.Stadia:
                        break;
                    case RuntimePlatform.CloudRendering:
                        break;
                    case RuntimePlatform.GameCoreXboxSeries:
                        break;
                    case RuntimePlatform.GameCoreXboxOne:
                        break;
                    case RuntimePlatform.PS5:
                        break;
                    case RuntimePlatform.EmbeddedLinuxArm64:
                        break;
                    case RuntimePlatform.EmbeddedLinuxArm32:
                        break;
                    case RuntimePlatform.EmbeddedLinuxX64:
                        break;
                    case RuntimePlatform.EmbeddedLinuxX86:
                        break;
                    case RuntimePlatform.LinuxServer:
                        break;
                    case RuntimePlatform.WindowsServer:
                        break;
                    case RuntimePlatform.OSXServer:
                        break;
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}