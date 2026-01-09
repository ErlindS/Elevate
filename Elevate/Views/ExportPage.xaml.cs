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