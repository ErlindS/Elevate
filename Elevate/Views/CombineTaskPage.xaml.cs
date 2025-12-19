using Microsoft.Maui.Controls; // Make sure this is included for ContentPage
using Elevate.ViewModels;
using Elevate.Models;
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