namespace ET
{
    public static class TaskDataSystem
    {
        public static TaskProto ToTaskProto(this TaskData self)
        {
            return new TaskProto()
            {
                Id = (int) self.Id,
                Args = self.Args,
                Min = self.Min,
                Max = self.Max,
                Status = (int) self.Status,
                Time = self.FinishTime,
            };
        }
        
        public static void ToTask(this TaskData self, TaskProto task)
        {
            
        }
    }
}