namespace app.models
{
    public class TaskModel
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public string CreateDate { get; set; }
        public string SetDate { get; set; }

        public TaskModel(string name, string setDate, string desc = "")
        {
            Name = name;
            Desc = desc;
            CreateDate = DateTime.Today.ToString("dd/MM/yyyy");
            SetDate = setDate;
        }
        public void Edit(string name, string setDate, string desc = "")
        {
            Name = name;
            Desc = desc;
            SetDate = setDate;
        }
    }
}
