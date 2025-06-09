using Elevate.Services;

namespace Elevate
{
    public partial class Analytic : ContentPage
    {
        private ElevateTaskService _taskService;
        public Analytic(ElevateTaskService taskService)
        {
            InitializeComponent();

            _taskService = taskService;
            TodoItemsCollectionView.ItemsSource = _taskService.Tasks;

        }
    }
}