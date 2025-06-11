using CommunityToolkit.Mvvm.ComponentModel;
using Elevate.Models;
using Elevate.Services;

namespace Elevate.ViewModels
{
    public partial class AddTaskViewModel : ObservableObject
    {
        private ElevateTaskService _taskService;

        //[ObservableProperty]
        //private string _newTodoText = string.Empty; // Holds the text for the new task entry

        //[ObservableProperty]
        //private string _newTodoHours = string.Empty; // Holds the hours for the new task entry

        //// Properties for filter checkboxes (assuming these control filters for the list)
        //[ObservableProperty]
        //private bool _isProjectFilterActive;

        //[ObservableProperty]
        //private bool _isDueTodayFilterActive;

        //[ObservableProperty]
        //private bool _isAtomicFilterActive;


        public AddTaskViewModel(ElevateTaskService taskService)
        {
            _taskService = taskService;
            //TodoItemsCollectionView.ItemsSource = _taskService.Tasks;
        }

        /*
        private void OnAddTodoClicked(object sender, EventArgs e)
        {
            string newTodoText = NewTodoEntry.Text?.Trim(); // Hole den Text aus dem Eingabefeld

            if (!string.IsNullOrWhiteSpace(newTodoText)) // Überprüfe, ob der Text nicht leer oder nur Leerzeichen ist
            {
                _taskService.Tasks.Add(new ElevateTask(newTodoText, true, "11:00", "12:00")); 
                NewTodoEntry.Text = string.Empty; // Eingabefeld leeren
            }
        }

        // Methode, die aufgerufen wird, wenn der Status einer Checkbox geändert wird
        private void OnTodoStatusChanged(object sender, CheckedChangedEventArgs e)
        {
            // Finde heraus, welches TodoItem zu dieser Checkbox gehört
            if (sender is CheckBox checkBox && checkBox.BindingContext is TodoItem todoItem)
            {
                todoItem.IsDone = e.Value; // Aktualisiere den IsDone-Status des TodoItem
            }
        }

        // Methode, die aufgerufen wird, wenn der "Löschen"-Button geklickt wird
        private void OnDeleteTodoClicked(object sender, EventArgs e)
        {
            // Finde heraus, welches TodoItem zu diesem Button gehört
            if (sender is Button button && button.BindingContext is TodoItem todoItemToDelete)
            {
                //_todoItems.Remove(todoItemToDelete); // Entferne das TodoItem aus der Liste
            }
        }*/
    }

}
