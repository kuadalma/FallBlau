namespace api.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreateDate { get; set; }
        public string SetDate { get; set; }
        public bool IsCompleted { get; set; }
        public virtual User? User { get; set; }
    }
}
