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

    private void OnTimeSlotDrop(object sender, DropEventArgs e)
    {
        if (sender is Element dropTarget && dropTarget.BindingContext is TimeSlot timeSlot)
        {
            // Determine which column (day indicator) the drop hit 
            int colIndex = Grid.GetColumn(dropTarget);
            
            // Columns 1-7 represent days 0-6 in WeekDays array
            int dayIndex = colIndex - 1;

            if (BindingContext is WeeklyCalendarViewModel viewModel && 
                viewModel.WeekDays != null && 
                dayIndex >= 0 && dayIndex < viewModel.WeekDays.Count)
            {
                var parameter = new DropTaskParameter
                {
                    DayColumn = viewModel.WeekDays[dayIndex],
                    TimeSlot = timeSlot
                };

                viewModel.DropTaskOnTimeSlotCommand.Execute(parameter);
            }
        }
    }
}