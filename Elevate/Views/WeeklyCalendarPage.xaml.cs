using Elevate.ViewModels;

namespace Elevate
{
    public partial class WeeklyCalendarPage : ContentPage
    {
        public WeeklyCalendarPage(WeeklyCalendarViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}