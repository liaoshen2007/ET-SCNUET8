namespace ET
{
    public static class TaskDataSystem
    {
        public static TaskProto ToTaskProto(this TaskData self)
        {
            return new TaskProto()
            {
                Id = (int)self.Id,
                Args = self.Args,
                Min = self.Min,
                Max = self.Max,
                Status = (int)self.Status,
                Time = self.FinishTime,
                AcceptTime = self.AcceptTime,
            };
        }

        public static void ToTask(this TaskData self, TaskProto task)
        {
            self.Args = task.Args;
            self.Min = task.Min;
            self.Max = task.Max;
            self.Status = (TaskStatus)task.Status;
            self.FinishTime = task.Time;
            self.AcceptTime = task.AcceptTime;
        }
    }
}