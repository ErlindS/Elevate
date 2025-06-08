using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elevate.Models;
using System.Collections.ObjectModel;
using Elevate.Services;

namespace Elevate
{
    public partial class TodaysTask : ContentPage
    {
        private ObservableCollection<IElevateTaskComponent> _todoItems;
        private ElevateTaskService _taskService;
        public TodaysTask(ElevateTaskService taskService)
        {
            InitializeComponent();

            _taskService = taskService;

            TodoItemsCollectionView.ItemsSource = _taskService.Tasks;

        }

    }

}
