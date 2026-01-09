using Elevate.ViewModels;
namespace Elevate
{
    public partial class CombineTaskPage : ContentPage
    {
        public CombineTaskPage(CombineTaskViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }


        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            string oldText = e.OldTextValue;
            string newText = e.NewTextValue;
        }
    }
}