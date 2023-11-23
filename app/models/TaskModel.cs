namespace app.models
{
    public class TaskModel
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime SetDate { get; set; }

        public TaskModel(string name, DateTime setDate, string desc = "")
        {
            Name = name;
            Desc = desc;
            CreateDate = DateTime.Today;
            SetDate = setDate;
        }
        public void Edit(string name, DateTime setDate, string desc = "")
        {
            Name = name;
            Desc = desc;
            SetDate = setDate;
        }
    }
}
