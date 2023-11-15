using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        private const string FileName = "tasks.json";

        public ObservableCollection<TaskItem> Tasks { get; set; }

        public MainPage()
        {
            InitializeComponent();
            Tasks = new ObservableCollection<TaskItem>();
            taskListView.ItemsSource = Tasks;

            // Load tasks from file when the app starts
            LoadTasksFromFile();
        }

        private void OnAddTaskClicked(object sender, EventArgs e)
        {
            string taskDescription = taskEntry.Text;
            DateTime dueDate = dueDatePicker.Date;

            if (!string.IsNullOrWhiteSpace(taskDescription))
            {
                TaskItem newTask = new TaskItem
                {
                    Description = taskDescription,
                    DueDate = dueDate
                };

                Tasks.Add(newTask);

                // Save tasks to file after adding a new task
                SaveTasksToFile();
            }

            taskEntry.Text = string.Empty;
        }

        private void SaveTasksToFile()
        {
            string json = JsonConvert.SerializeObject(Tasks);

            // Get the path to the app's local folder
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FileName);

            // Write the JSON string to the file
            File.WriteAllText(filePath, json);

            // Update the file path label
            pathLabel.Text = $"File Path: {filePath}";
        }

        private void LoadTasksFromFile()
        {
            // Get the path to the app's local folder
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FileName);

            // Check if the file exists
            if (File.Exists(filePath))
            {
                // Read the JSON string from the file
                string json = File.ReadAllText(filePath);

                // Deserialize the JSON string to a list of TaskItem objects
                ObservableCollection<TaskItem> loadedTasks = JsonConvert.DeserializeObject<ObservableCollection<TaskItem>>(json);

                // Update the Tasks collection with the loaded tasks
                Tasks.Clear();
                foreach (TaskItem task in loadedTasks)
                {
                    Tasks.Add(task);
                }
            }


            // Update the file path label
            pathLabel.Text = $"File Path: {filePath}";
        }
    }

    public class TaskItem
    {
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}