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
            ProjectsCollectionView.ItemsSource = _taskService.Projects;
        }

        private async void OwnDragStarting(object sender, DragStartingEventArgs e)
        {
            //await DisplayAlert("1", "2", "3");
        }
        private async void OwnDropOver(object sender, DragStartingEventArgs e)
        {
            // await DisplayAlert("1", "2", "3");
        }
        private async void OwnDropGesture(object sender, DragStartingEventArgs e)
        {
            // await DisplayAlert("1", "2", "3");
        }
    }
}