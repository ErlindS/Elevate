using Elevate.ViewModels;

namespace Elevate.Views;

public partial class CompletedHousesPage : ContentPage
{
    private readonly CompletedHousesViewModel _viewModel;

    public CompletedHousesPage(CompletedHousesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;

        // Bind the summary label to the ViewModel
        SummaryLabel.SetBinding(Label.TextProperty, "CompletionSummary");

        // Load the houses when the page appears
        this.Loaded += (s, e) =>
        {
            _viewModel.LoadCompletedTasksCommand.Execute(null);
        };
    }
}