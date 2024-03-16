namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class M2C_PathfindingResultHandler : MessageHandler<Scene, M2C_PathfindingResult>
	{
		protected override async ETTask Run(Scene root, M2C_PathfindingResult message)
		{
			//Log.Error("message.Points"+message.Points.Count);
			Unit unit = root.CurrentScene().GetComponent<UnitComponent>().Get(message.Id);
			float speed = 6000f;
			if (unit == null)
			{
				Log.Error("robot is here!!:"+message.Id);
			}
			else
			{
				speed = unit.GetComponent<NumericComponent>().GetAsFloat(NumericType.Speed);
				await unit.GetComponent<MoveComponent>().MoveToAsync(message.Points, speed);
			}
		}
	}
}
