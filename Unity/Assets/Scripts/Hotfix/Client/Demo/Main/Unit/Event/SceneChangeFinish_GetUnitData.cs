namespace ET.Client
{
    [Event(SceneType.Client)]
    public class SceneChangeFinish_GetUnitData: AEvent<Scene, SceneChangeFinish>
    {
        protected override async ETTask Run(Scene scene, SceneChangeFinish a)
        {
            if (a.UnitId<10000)
            {
                Log.Error("I am robot,so i need to stop"+a.UnitId);
                return;
            }
            
            var resp = await scene.GetComponent<ClientSenderComponent>().Call(new C2M_GetPlayerData()) as M2C_GetPlayerData;
            scene.GetComponent<ClientTaskComponent>().AddUpdateTask(resp.TaskList);
            scene.GetComponent<ClientTaskComponent>().UpdateFinishTask(resp.FinishDict);
            scene.GetComponent<ClientItemComponent>().AddUpdateItem(resp.ItemList);
        }
    }
}