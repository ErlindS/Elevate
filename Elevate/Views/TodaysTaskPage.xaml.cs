using Elevate.ViewModels;

namespace Elevate
{
    public partial class TodaysTaskPage : ContentPage
    {
        public TodaysTaskPage(TodaysTaskViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}