using Elevate.Services;
using Elevate.Models;

namespace Elevate
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            var service = new TaskServices();
            var tasks = service.GetAllTasks();
            TasksView.ItemsSource = tasks;
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }


    }

}
