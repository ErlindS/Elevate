using Microsoft.Maui.Controls; // Make sure this is included for ContentPage
using Elevate.ViewModels;

namespace Elevate
{
    public partial class ExportPage : ContentPage
    {
        public ExportPage(ExportViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}