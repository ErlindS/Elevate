using Microsoft.Maui.Controls; // Make sure this is included for ContentPage
using Elevate.ViewModels;
namespace Elevate
{
    public partial class AnalyticsPage : ContentPage
    {
        public AnalyticsPage(AnalyticsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel; // This correctly sets the BindingContext via DI
        }
    }
}