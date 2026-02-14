using Elevate.ViewModels;

namespace Elevate.Views;

public partial class WeeklyCalendarPage : ContentPage
{
    public WeeklyCalendarPage(WeeklyCalendarViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is WeeklyCalendarViewModel viewModel)
        {
            viewModel.LoadWeeklyTasksCommand.Execute(null);
        }
    }
}