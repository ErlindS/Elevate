using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Elevate.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        // Property für die Liste der TODOs. ObservableCollection ist wichtig!
        public ObservableCollection<TodoItem> TodoItems { get; set; }

        // Property für den Text des neuen TODOs.
        // [ObservableProperty] generiert automatisch eine backing field und implementiert INotifyPropertyChanged
        [ObservableProperty]
        private string newTodoText;

        public MainPageViewModel()
        {
            TodoItems = new ObservableCollection<TodoItem>();

            // Beispiel-TODOs hinzufügen
            TodoItems.Add(new TodoItem { Text = "MAUI App mit MVVM erstellen", IsDone = false });
            TodoItems.Add(new TodoItem { Text = "CommunityToolkit.Mvvm lernen", IsDone = true });
            TodoItems.Add(new TodoItem { Text = "UI und Logik trennen", IsDone = false });

            // Initialisierung der Commands
            AddTodoCommand = new RelayCommand(AddTodo);
            DeleteTodoCommand = new RelayCommand<TodoItem>(DeleteTodo);
            // Für das CheckedChanged Event der Checkbox brauchen wir keinen expliziten Command,
            // da das Binding direkt auf IsDone wirkt und IsDone ein Property des TodoItem Models ist.
            // Später könnten wir hier aber Logic für Speichern etc. einfügen.
        }

        // Command für den "Hinzufügen"-Button
        public ICommand AddTodoCommand { get; }

        private void AddTodo()
        {
            if (!string.IsNullOrWhiteSpace(NewTodoText))
            {
                TodoItems.Add(new TodoItem { Text = NewTodoText, IsDone = false });
                NewTodoText = string.Empty; // Leert das Eingabefeld in der UI dank Binding und ObservableProperty
            }
        }

        // Command für den "Löschen"-Button.
        // RelayCommand<T> erwartet ein Parameter vom Typ T.
        public ICommand DeleteTodoCommand { get; }

        private void DeleteTodo(TodoItem todoItemToDelete)
        {
            if (todoItemToDelete != null)
            {
                TodoItems.Remove(todoItemToDelete);
            }
        }

        // Optional: Wenn wir auf Änderungen im TodoItem reagieren wollen,
        // z.B. zum Speichern, wenn IsDone geändert wird.
        // Das TodoItem Model müsste hierfür INotifyPropertyChanged implementieren
        // oder w
    }
 }
