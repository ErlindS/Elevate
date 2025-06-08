using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elevate.Models;
using System.Collections.ObjectModel;
using Elevate.Services;

namespace Elevate
{
    public partial class AddTask : ContentPage
    {
        private ElevateTaskService _taskService;


        public AddTask(ElevateTaskService taskService)
        {
            InitializeComponent();

            _taskService = taskService;
            TodoItemsCollectionView.ItemsSource = _taskService.Tasks;

        }
        private void OnAddTodoClicked(object sender, EventArgs e)
        {
            string newTodoText = NewTodoEntry.Text?.Trim(); // Hole den Text aus dem Eingabefeld

            if (!string.IsNullOrWhiteSpace(newTodoText)) // Überprüfe, ob der Text nicht leer oder nur Leerzeichen ist
            {
                _taskService.Tasks.Add(new ElevateTask(newTodoText, true));
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
        }
    }

}
