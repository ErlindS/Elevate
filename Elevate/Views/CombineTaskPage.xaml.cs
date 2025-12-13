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
            BindingContext = viewModel; // This correctly sets the BindingContext via DI
        }

        // Inside CombineTaskPage.xaml.cs

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            // Optional: You can get the old and new text here
            string oldText = e.OldTextValue;
            string newText = e.NewTextValue;

            // Example: Only do something if you want validation
            // System.Diagnostics.Debug.WriteLine($"User is typing: {newText}");
        }
    }
}