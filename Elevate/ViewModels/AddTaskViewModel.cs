using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elevate.Models;
using Elevate.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq; // Wichtig für .Where und .Select

namespace Elevate.ViewModels
{
    // Die Hilfsklasse für deine dynamischen Textfelder
    public partial class FitCriterionModel : ObservableObject
    {
        [ObservableProperty]
        private string text = string.Empty;
    }

    public partial class AddTaskViewModel : ObservableObject
    {
        [ObservableProperty]
        private string newTodoText = string.Empty;

        [ObservableProperty]
        private int newTaskPriority = 1;

        [ObservableProperty]
        private string newTaskDescription = string.Empty;

        [ObservableProperty]
        private string newTaskCategory = string.Empty;

        [ObservableProperty]
        private string selectedDay = "Today";

        [ObservableProperty]
        private ObservableCollection<string> availableDays = new();

        [ObservableProperty]
        private ElevateTaskService taskService = new();

        [ObservableProperty]
        private ElevateTask tasks = new();

        // Startet mit einem leeren Feld, damit der Nutzer sofort tippen kann
        [ObservableProperty]
        private ObservableCollection<FitCriterionModel> fitCriteria = new() { new FitCriterionModel() };

        public AddTaskViewModel(ElevateTaskService taskService)
        {
            TaskService = taskService;
            Tasks = taskService.unsortedTasks;
            InitializeDays();
        }

        private void InitializeDays()
        {
            AvailableDays.Clear();
            AvailableDays.Add("Today");
            AvailableDays.Add("Monday");
            AvailableDays.Add("Tuesday");
            AvailableDays.Add("Wednesday");
            AvailableDays.Add("Thursday");
            AvailableDays.Add("Friday");
            AvailableDays.Add("Saturday");
            AvailableDays.Add("Sunday");
        }

        [RelayCommand]
        private void AddItem()
        {
            if (string.IsNullOrWhiteSpace(NewTodoText))
                return;

            DateTime scheduledDate = GetDateForDay(SelectedDay);

            // Dynamische Felder in eine reine String-Liste umwandeln, leere Felder ignorieren
            List<string> criteriaStrings = FitCriteria
                .Where(c => !string.IsNullOrWhiteSpace(c.Text))
                .Select(c => c.Text)
                .ToList();

            ElevateTask newTask = new ElevateTask
            {
                Name = NewTodoText,
                Priority = NewTaskPriority,
                Description = NewTaskDescription,
                Category = NewTaskCategory,
                ScheduledDate = scheduledDate,
                Fitcriteria = criteriaStrings,
                Id = UniqueIdGenerator.GenerateNewId()
            };

            if (Tasks.SubTasks == null)
            {
                Tasks.SubTasks = new();
            }
            Tasks.SubTasks.Add(newTask);

            // Formularfelder zurücksetzen
            NewTodoText = string.Empty;
            NewTaskPriority = 1;
            NewTaskDescription = string.Empty;
            NewTaskCategory = string.Empty;
            SelectedDay = "Today"; // Wieder auf "Today" setzen

            // WICHTIG: Die Liste der Kriterien wieder auf genau ein leeres Feld zurücksetzen!
            FitCriteria.Clear();
            FitCriteria.Add(new FitCriterionModel());
        }

        [RelayCommand]
        private void DeleteItem(int id)
        {
            var task = Tasks.SubTasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return;

            Tasks.SubTasks.Remove(task);
        }

        [RelayCommand]
        private void CheckAndAddNewCriterion(FitCriterionModel currentItem)
        {
            // Wir prüfen: Hat das aktuelle Feld Text? UND ist es das LETZTE Feld in der Liste?
            if (!string.IsNullOrWhiteSpace(currentItem.Text) && FitCriteria.Last() == currentItem)
            {
                // Neues leeres Feld unten dranhängen
                FitCriteria.Add(new FitCriterionModel());
            }
        }

        private DateTime GetDateForDay(string dayName)
        {
            DateTime today = DateTime.Now;

            // WICHTIG: "Today" muss extra abgefangen werden, da es kein DayOfWeek-Enum ist!
            if (dayName == "Today")
            {
                return today;
            }

            DayOfWeek targetDay = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), dayName);

            int daysAhead = (int)targetDay - (int)today.DayOfWeek;

            // Wenn der Tag heute oder in der Vergangenheit dieser Woche lag, springe in die nächste Woche
            if (daysAhead <= 0)
            {
                daysAhead += 7;
            }

            return today.AddDays(daysAhead);
        }
    }
}