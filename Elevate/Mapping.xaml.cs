using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Elevate;

// Removed 'ObservableObject' as a base class since 'ContentPage' is already a base class.
// Instead, use the `[ObservableObject]` attribute to enable observable properties.
[ObservableObject]
public partial class Mappping : ContentPage
{
    public ObservableCollection<TaskModel> TaskList { get; set; } = new ObservableCollection<TaskModel>();

    private List<TaskModel> _allTaskList = new List<TaskModel>();

    [ObservableProperty]
    private int _selectedOption;

    public Mappping()
    {
        _allTaskList.Add(new TaskModel() { TaskName = "Task 1", TaskStatus = (int)TaskStatusOption.NewTask, TaskId = 1 });
        _allTaskList.Add(new TaskModel() { TaskName = "Task 2", TaskStatus = (int)TaskStatusOption.NewTask, TaskId = 2 });
        _allTaskList.Add(new TaskModel() { TaskName = "Task 3", TaskStatus = (int)TaskStatusOption.NewTask, TaskId = 3 });

        _allTaskList.Add(new TaskModel() { TaskName = "Task 4", TaskStatus = (int)TaskStatusOption.InProgress, TaskId = 1 });
        _allTaskList.Add(new TaskModel() { TaskName = "Task 5", TaskStatus = (int)TaskStatusOption.InProgress, TaskId = 1 });

        _allTaskList.Add(new TaskModel() { TaskName = "Task 6", TaskStatus = (int)TaskStatusOption.InReview, TaskId = 1 });
        _allTaskList.Add(new TaskModel() { TaskName = "Task 7", TaskStatus = (int)TaskStatusOption.InReview, TaskId = 1 });

        _allTaskList.Add(new TaskModel() { TaskName = "Task 8", TaskStatus = (int)TaskStatusOption.Done, TaskId = 1 });
        InitializeComponent();

        AddTaskList();
    }

    private void AddTaskList()
    {
        var filterTaskList = _allTaskList.Where(f => f.TaskStatus == SelectedOption).ToList();

        TaskList.Clear();
        foreach(var task in filterTaskList)
        {
            TaskList.Add(task);
        }
    }
}
