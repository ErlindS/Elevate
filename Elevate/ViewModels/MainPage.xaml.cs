using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elevate.Models;
using System.Collections.ObjectModel;

namespace Elevate
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<TodoItem> _todoItems;

        public MainPage()
        {
            InitializeComponent();

            // Initialisiere die Liste der TODOs
            _todoItems = new ObservableCollection<TodoItem>();
            // Verbinde die CollectionView mit unserer Liste
            TodoItemsCollectionView.ItemsSource = _todoItems;

            // Beispiel-TODOs hinzufügen (kann später entfernt werden)
            _todoItems.Add(new TodoItem { Text = "MAUI App erstellen", IsDone = false });
            _todoItems.Add(new TodoItem { Text = "Einkaufen gehen", IsDone = true });
            _todoItems.Add(new TodoItem { Text = "Freunde anrufen", IsDone = false });
        }

        // Methode, die aufgerufen wird, wenn der "Hinzufügen"-Button geklickt wird
        private void OnAddTodoClicked(object sender, EventArgs e)
        {
            string newTodoText = NewTodoEntry.Text?.Trim(); // Hole den Text aus dem Eingabefeld

            if (!string.IsNullOrWhiteSpace(newTodoText)) // Überprüfe, ob der Text nicht leer oder nur Leerzeichen ist
            {
                _todoItems.Add(new TodoItem { Text = newTodoText, IsDone = false });
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
                // In diesem Fall brauchen wir keine explizite UI-Aktualisierung,
                // da das Binding von selbst funktioniert.
            }
        }

        // Methode, die aufgerufen wird, wenn der "Löschen"-Button geklickt wird
        private void OnDeleteTodoClicked(object sender, EventArgs e)
        {
            // Finde heraus, welches TodoItem zu diesem Button gehört
            if (sender is Button button && button.BindingContext is TodoItem todoItemToDelete)
            {
                _todoItems.Remove(todoItemToDelete); // Entferne das TodoItem aus der Liste
            }
        }
    }

}
